// Enhanced email parsing with weighted scoring system
const items = $input.all();
const results = [];

for (const item of items) {
  const data = item.json;
  const pendingApplications = data.pendingApplications || [];
  
  // Extract email details
  const subject = (data.subject || '').toLowerCase();
  const snippet = (data.snippet || '').toLowerCase();
  const from = data.from || '';
  const senderEmail = from.toLowerCase();
  const body = (data.textPlain || data.textHtml || '').toLowerCase();
  const fullText = `${subject} ${snippet} ${body}`;
  
  // Build expected companies database from pending applications
  const expectedCompanies = pendingApplications.map(app => ({
    id: app.applicationId,
    name: (app.companyName || app.company?.name || '').toLowerCase(),
    jobTitle: (app.jobTitle || '').toLowerCase(),
    // Extract potential domains from company name
    domains: extractPotentialDomains(app.companyName || app.company?.name || '')
  }));
  
  // Helper: Extract potential domains from company name
  function extractPotentialDomains(companyName) {
    if (!companyName) return [];
    const clean = companyName.toLowerCase()
      .replace(/\s+inc\.?|\s+llc\.?|\s+ltd\.?|\s+corp\.?/gi, '')
      .replace(/[^a-z0-9\s]/g, '')
      .trim();
    const words = clean.split(/\s+/);
    return [
      clean.replace(/\s+/g, ''),  // "Microsoft Corporation" -> "microsoft"
      ...words  // ["microsoft", "corporation"]
    ];
  }
  
  // Extract sender domain
  const senderDomainMatch = senderEmail.match(/@([^>\s]+)/);
  const senderDomain = senderDomainMatch ? senderDomainMatch[1].toLowerCase() : '';
  
  // Known ATS platforms with high trust
  const trustedATSPlatforms = [
    'greenhouse.io',
    'lever.co',
    'workday.com',
    'icims.com',
    'taleo.net',
    'jobvite.com',
    'smartrecruiters.com',
    'breezy.hr',
    'recruitee.com',
    'myworkdayjobs.com',
    'ultipro.com'
  ];
  
  // Job boards (medium trust - could be promotional)
  const jobBoards = [
    'linkedin.com',
    'indeed.com',
    'glassdoor.com',
    'ziprecruiter.com',
    'monster.com',
    'careerbuilder.com'
  ];
  
  // NEGATIVE PATTERNS - Auto-discard if matched
  const negativePatterns = [
    /unsubscribe/i,
    /click here to view/i,
    /view this email in your browser/i,
    /you('re| are) receiving this (email|newsletter)/i,
    /(weekly|daily|monthly) (digest|newsletter|roundup)/i,
    /job (alerts?|recommendations?|matches?)/i,
    /out of office/i,
    /automatic reply/i,
    /delivery status notification/i,
    /mailer-daemon/i,
    /tips (for|to)/i,
    /how to (ace|prepare|succeed)/i,
    /\d+ (new )?jobs? (available|matching|in)/i,  // "50 jobs available"
    /apply now/i,  // Usually promotional
    /limited time/i,
    /don't miss out/i
  ];
  
  // Check negative patterns first
  const hasNegativePattern = negativePatterns.some(pattern => 
    pattern.test(fullText)
  );
  
  if (hasNegativePattern) {
    continue; // Skip this email
  }
  
  // SCORING SYSTEM
  let totalScore = 0;
  let maxScore = 0;
  let matchedCompany = null;
  let matchReasons = [];
  
  // FACTOR 1: Company Name Match with Job Title Disambiguation (HIGH WEIGHT: 50 points)
  maxScore += 50;
  let companyMatchScore = 0;
  
  // First pass: Find all companies that match by name or domain
  const companyMatches = [];
  
  for (const company of expectedCompanies) {
    if (!company.name) continue;
    
    // Check for exact company name in email
    if (fullText.includes(company.name)) {
      companyMatches.push({
        company: company,
        matchType: 'name',
        score: 50
      });
    }
    // Check for domain match
    else {
      for (const domain of company.domains) {
        if (domain && senderDomain.includes(domain)) {
          companyMatches.push({
            company: company,
            matchType: 'domain',
            score: 45
          });
          break;
        }
      }
    }
  }
  
  // If we found matches, determine the best one
  if (companyMatches.length > 0) {
    // If multiple applications for same company, use job title as tiebreaker
    if (companyMatches.length > 1) {
      // Check if any job title appears in the email
      for (const match of companyMatches) {
        const jobTitle = match.company.jobTitle;
        if (jobTitle && fullText.includes(jobTitle)) {
          matchedCompany = match.company;
          companyMatchScore = match.score;
          matchReasons.push(`Company "${match.company.name}" + job title "${jobTitle}" found in email`);
          break;
        }
      }
      
      // If no job title matched, take the first match (fallback to original behavior)
      if (!matchedCompany) {
        matchedCompany = companyMatches[0].company;
        companyMatchScore = companyMatches[0].score;
        matchReasons.push(`Company "${matchedCompany.name}" found in email (${companyMatches.length} applications, no job title match - using first)`);
      }
    } else {
      // Only one match, use it
      matchedCompany = companyMatches[0].company;
      companyMatchScore = companyMatches[0].score;
      const matchType = companyMatches[0].matchType === 'name' 
        ? `Company name "${matchedCompany.name}" found in email`
        : `Sender domain "${senderDomain}" matches company "${matchedCompany.name}"`;
      matchReasons.push(matchType);
    }
  }
  
  totalScore += companyMatchScore;
  
  // FACTOR 2: Trusted ATS Platform (MEDIUM WEIGHT: 20 points)
  maxScore += 20;
  let atsScore = 0;
  const matchedATS = trustedATSPlatforms.find(ats => senderDomain.includes(ats));
  if (matchedATS) {
    atsScore = 20;
    matchReasons.push(`Email from trusted ATS platform: ${matchedATS}`);
  } else if (jobBoards.some(board => senderDomain.includes(board))) {
    atsScore = 5;  // Lower score for job boards
    matchReasons.push(`Email from job board`);
  }
  totalScore += atsScore;
  
  // FACTOR 3: Status Keywords with Context (VARIABLE WEIGHT: up to 30 points)
  maxScore += 30;
  let keywordScore = 0;
  let detectedStatus = null;
  let detectedStage = null;
  let bestPatternScore = 0;
  
  // Enhanced patterns with context-aware matching
  // Status: Accepted, Pending, Rejected (ApplicationDecisionStatus)
  // Stage: Applied, PhoneScreen, Assessment, HrInterview, Offer, TechnicalInterview (ApplicationStage)
  const statusPatterns = [
    {
      // Application Received
      positiveKeywords: ['received your application', 'application submitted', 'thank you for applying', 'application complete'],
      requiredContext: ['application', 'received'],
      status: 'Pending',
      stage: 'Applied',
      score: 25
    },
    {
      // Phone Screen / Initial Interview
      positiveKeywords: ['phone screen', 'initial call', 'brief call', 'phone interview', 'introductory call'],
      requiredContext: [],
      negativeKeywords: ['tips for', 'how to'],
      status: 'Pending',
      stage: 'PhoneScreen',
      score: 30
    },
    {
      // Technical Interview
      positiveKeywords: ['technical interview', 'coding interview', 'technical round', 'technical discussion', 'engineering interview'],
      requiredContext: [],
      negativeKeywords: ['tips for', 'how to'],
      status: 'Pending',
      stage: 'TechnicalInterview',
      score: 30
    },
    {
      // HR Interview
      positiveKeywords: ['hr interview', 'behavioral interview', 'culture fit', 'meet with hr', 'hr round'],
      requiredContext: [],
      negativeKeywords: ['tips for', 'how to'],
      status: 'Pending',
      stage: 'HrInterview',
      score: 30
    },
    {
      // Generic Interview (when type unclear)
      positiveKeywords: ['invite you to interview', 'schedule an interview', 'interview invitation', 'would like to meet', 'discuss your application', 'next interview'],
      requiredContext: ['interview'],
      negativeKeywords: ['tips for interview', 'how to interview'],
      status: 'Pending',
      stage: 'PhoneScreen',  // Default to PhoneScreen for generic interviews
      score: 28
    },
    {
      // Assessment
      positiveKeywords: ['complete the assessment', 'technical challenge', 'coding test', 'take-home assignment', 'assessment link', 'technical exam', 'online assessment', 'skills assessment', 'assess your', 'passed the screening', 'next step', 'technical evaluation', 'evaluation of your skills'],
      requiredContext: [],  // Removed strict requirement - phrases are specific enough
      negativeKeywords: ['tips for assessment', 'how to prepare for'],
      status: 'Pending',
      stage: 'Assessment',
      score: 28
    },
    {
      // Offer (HIGHEST CONFIDENCE)
      positiveKeywords: ['pleased to offer', 'job offer', 'offer letter', 'congratulations', 'offer of employment'],
      requiredContext: ['offer'],
      negativeKeywords: ['if we can offer', 'unable to offer'],
      status: 'Accepted',
      stage: 'Offer',
      score: 30
    },
    {
      // Rejection (HIGH CONFIDENCE)
      positiveKeywords: ['not moving forward', 'decided to pursue', 'other candidates', 'not selected', 'will not be moving forward', 'you have been rejected', 'regret to inform', 'unfortunately', 'we have decided'],
      requiredContext: [],  // Removed strict requirement - let positive keywords drive matching
      negativeKeywords: ['if we do not', 'should you not', 'if you are not', 'tips', 'how to'],
      status: 'Rejected',
      stage: null,  // Don't change stage for rejection, backend will keep current stage
      score: 28
    }
  ];
  
  for (const pattern of statusPatterns) {
    let patternMatches = 0;
    
    // Check positive keywords
    for (const keyword of pattern.positiveKeywords) {
      if (fullText.includes(keyword)) {
        patternMatches++;
      }
    }
    
    // Check negative keywords (disqualifies pattern)
    if (pattern.negativeKeywords) {
      const hasNegative = pattern.negativeKeywords.some(neg => fullText.includes(neg));
      if (hasNegative) {
        continue; // Skip this pattern
      }
    }
    
    // Check required context
    const hasRequiredContext = pattern.requiredContext.every(ctx => fullText.includes(ctx));
    
    if (patternMatches > 0 && hasRequiredContext) {
      const patternScore = pattern.score * (patternMatches / pattern.positiveKeywords.length);
      
      if (patternScore > bestPatternScore) {
        bestPatternScore = patternScore;
        detectedStatus = pattern.status;
        detectedStage = pattern.stage;
        keywordScore = patternScore;
        const stageInfo = pattern.stage ? `stage: ${pattern.stage}` : `status: ${pattern.status}`;
        matchReasons.push(`Detected ${stageInfo} (${patternMatches} keyword matches)`);
      }
    }
  }
  totalScore += keywordScore;
  
  // Calculate confidence as percentage
  const confidence = maxScore > 0 ? (totalScore / maxScore) : 0;
  
  // THRESHOLD: Only proceed if confidence >= 55% AND we have a status
  // (Balanced threshold - company match + keywords can reach 80% without ATS)
  // Note: detectedStage can be null for rejections (backend keeps current stage)
  if (confidence >= 0.55 && detectedStatus) {
    // Extract company name for hint
    let companyNameHint = matchedCompany ? matchedCompany.name : null;
    
    // If no matched company but ATS platform, try to extract from domain
    if (!companyNameHint && matchedATS) {
      const domainParts = senderDomain.split('.');
      if (domainParts.length >= 2) {
        companyNameHint = domainParts[0]
          .replace(/noreply|mail|email|careers|recruiting/gi, '')
          .trim();
      }
    }
    
    results.push({
      json: {
        userId: data.userId,
        userEmail: data.userEmail,
        emailId: data.id,
        emailSubject: data.subject,
        emailFrom: from,
        emailDate: data.internalDate,
        detectedStatus: detectedStatus,
        detectedStage: detectedStage,
        confidence: Math.round(confidence * 100) / 100,  // Round to 2 decimals
        companyNameHint: companyNameHint,
        matchedApplicationId: matchedCompany?.id || null,
        snippet: data.snippet,
        matchReasons: matchReasons.join('; '),
        scoreBreakdown: {
          companyMatch: companyMatchScore,
          atsMatch: atsScore,
          keywordMatch: Math.round(keywordScore * 100) / 100,
          total: Math.round(totalScore * 100) / 100,
          maxPossible: maxScore
        },
        rawEmailData: {
          subject: data.subject,
          from: from,
          snippet: data.snippet
        }
      }
    });
  }
}

return results;

