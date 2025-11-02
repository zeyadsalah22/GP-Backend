# Gmail Listener Feature - Architecture Design

## 🎯 Overview
This document outlines the architecture for integrating Gmail monitoring into the Job Lander backend while maintaining the existing clean architecture pattern.

---

## 📊 Existing Architecture Pattern

```
┌─────────────────────────────────────────────────────────────┐
│                         Controllers                          │
│  (HTTP handling, Authorization, Request/Response mapping)   │
└────────────────────┬────────────────────────────────────────┘
                     │
                     ↓
┌─────────────────────────────────────────────────────────────┐
│                      Service Layer                           │
│            (Business logic, Validation, Orchestration)       │
│         Interfaces/ + Implements/ pattern                    │
└────────────────────┬────────────────────────────────────────┘
                     │
                     ↓
┌─────────────────────────────────────────────────────────────┐
│                    Repository Layer                          │
│         (Data access, EF Core queries, Persistence)          │
│         Interfaces/ + Implements/ pattern                    │
└────────────────────┬────────────────────────────────────────┘
                     │
                     ↓
┌─────────────────────────────────────────────────────────────┐
│                      Models (Entities)                       │
│              (EF Core entities, Relationships)               │
└─────────────────────────────────────────────────────────────┘

Supporting Components:
- DTOs: Data transfer objects for API contracts
- Profiles: AutoMapper configurations
- Migrations: Database schema versioning
```

---

## 🏗️ Proposed Gmail Feature Architecture

### 1. New Entities (Models)

#### A. GmailConnection Entity
**Purpose:** Store OAuth tokens and connection metadata for each user

```csharp
// Models/GmailConnection.cs
public class GmailConnection
{
    public int GmailConnectionId { get; set; }
    public int UserId { get; set; }
    
    // OAuth tokens (should be encrypted at rest)
    public string EncryptedAccessToken { get; set; } = null!;
    public string EncryptedRefreshToken { get; set; } = null!;
    public DateTime TokenExpiresAt { get; set; }
    
    // User's Gmail address
    public string GmailAddress { get; set; } = null!;
    
    // Monitoring metadata
    public DateTime? LastCheckedAt { get; set; }
    public bool IsActive { get; set; }
    public DateTime ConnectedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    // Soft delete pattern (consistent with existing models)
    public bool IsDeleted { get; set; }
    
    // Optimistic concurrency (consistent with existing models)
    public byte[] Rowversion { get; set; } = null!;
    
    // Navigation property
    public virtual User User { get; set; } = null!;
}
```

**Relationship:** One-to-One with User (a user can have only one Gmail connection)

#### B. EmailApplicationUpdate Entity (Optional - for audit trail)
**Purpose:** Track all email-triggered updates for debugging and user transparency

```csharp
// Models/EmailApplicationUpdate.cs
public class EmailApplicationUpdate
{
    public int EmailApplicationUpdateId { get; set; }
    public int ApplicationId { get; set; }
    public int UserId { get; set; }
    
    // Email metadata
    public string EmailId { get; set; } = null!;  // Gmail message ID
    public string EmailSubject { get; set; } = null!;
    public string EmailFrom { get; set; } = null!;
    public DateTime EmailDate { get; set; }
    public string? EmailSnippet { get; set; }
    
    // Detection metadata
    public ApplicationDecisionStatus? DetectedStatus { get; set; }
    public ApplicationStage? DetectedStage { get; set; }
    public decimal Confidence { get; set; }
    public string? CompanyNameHint { get; set; }
    
    // Update status
    public bool WasApplied { get; set; }  // Did we actually update the application?
    public string? FailureReason { get; set; }  // If not applied, why?
    
    public DateTime CreatedAt { get; set; }
    
    // Navigation properties
    public virtual Application Application { get; set; } = null!;
    public virtual User User { get; set; } = null!;
}
```

**Relationship:** Many-to-One with Application and User

---

### 2. Layer-by-Layer Implementation

#### Layer 1: Repository Layer

**A. IGmailConnectionRepository**
```csharp
// Repositories/Interfaces/IGmailConnectionRepository.cs
public interface IGmailConnectionRepository
{
    // Basic CRUD
    Task<GmailConnection?> GetByUserIdAsync(int userId);
    Task<GmailConnection?> GetByGmailAddressAsync(string gmailAddress);
    Task<IEnumerable<GmailConnection>> GetAllActiveConnectionsAsync();
    Task<int> CreateAsync(GmailConnection connection);
    Task<bool> UpdateAsync(GmailConnection connection);
    Task<bool> DeleteAsync(int userId);
    
    // Specific queries
    Task<bool> ExistsForUserAsync(int userId);
    Task<bool> UpdateLastCheckedAsync(int userId, DateTime lastChecked);
}
```

**B. IEmailApplicationUpdateRepository** (Optional)
```csharp
// Repositories/Interfaces/IEmailApplicationUpdateRepository.cs
public interface IEmailApplicationUpdateRepository
{
    Task<int> CreateAsync(EmailApplicationUpdate update);
    Task<IEnumerable<EmailApplicationUpdate>> GetByApplicationIdAsync(int applicationId);
    Task<IEnumerable<EmailApplicationUpdate>> GetByUserIdAsync(int userId, int limit = 50);
    Task<bool> EmailAlreadyProcessedAsync(string emailId, int userId);
}
```

#### Layer 2: Service Layer

**A. IGmailService**
```csharp
// Services/Interfaces/IGmailService.cs
public interface IGmailService
{
    // OAuth flow
    Task<string> GenerateOAuthUrlAsync(int userId);
    Task<bool> HandleOAuthCallbackAsync(int userId, string code);
    Task<bool> DisconnectGmailAsync(int userId);
    
    // Connection management
    Task<GmailConnectionResponseDto?> GetConnectionStatusAsync(int userId);
    Task<IEnumerable<ActiveGmailConnectionDto>> GetAllActiveConnectionsForN8nAsync();
    
    // Token management
    Task<string> GetDecryptedAccessTokenAsync(int userId);
    Task<bool> RefreshAccessTokenAsync(int userId);
    
    // Monitoring
    Task<bool> UpdateLastCheckedAsync(int userId, DateTime lastChecked);
}
```

**B. IEmailProcessingService**
```csharp
// Services/Interfaces/IEmailProcessingService.cs
public interface IEmailProcessingService
{
    // Main entry point from n8n
    Task<EmailProcessingResultDto> ProcessEmailUpdateAsync(EmailUpdateWebhookDto webhookData);
    
    // Application matching logic
    Task<Application?> FindMatchingApplicationAsync(int userId, string companyNameHint, string emailContent);
    
    // Update application from email
    Task<bool> UpdateApplicationFromEmailAsync(
        int applicationId, 
        int userId, 
        ApplicationDecisionStatus? newStatus, 
        ApplicationStage? newStage,
        string emailSubject,
        string emailFrom);
}
```

**C. Extend IApplicationService** (if needed)
```csharp
// Add to existing IApplicationService
Task<Application?> FindByCompanyNameAndJobTitleAsync(int userId, string companyName, string? jobTitle = null);
```

#### Layer 3: Controller Layer

**A. GmailController**
```csharp
// Controllers/GmailController.cs
[ApiController]
[Route("api/[controller]")]
public class GmailController : ControllerBase
{
    // User-facing endpoints
    [HttpGet("connect")]
    [Authorize(Policy = "UserOrAdmin")]
    
    [HttpPost("callback")]
    [Authorize(Policy = "UserOrAdmin")]
    
    [HttpDelete("disconnect")]
    [Authorize(Policy = "UserOrAdmin")]
    
    [HttpGet("status")]
    [Authorize(Policy = "UserOrAdmin")]
    
    // n8n-facing endpoints (protected by API key)
    [HttpGet("active-connections")]
    [AllowAnonymous]  // Use custom API key auth
    
    [HttpPost("webhook/email-update")]
    [AllowAnonymous]  // Use custom API key auth
    
    [HttpPost("update-last-checked")]
    [AllowAnonymous]  // Use custom API key auth
}
```

---

### 3. Data Flow Diagrams

#### Flow 1: User Connects Gmail (OAuth)

```
┌─────────┐     1. Click          ┌──────────┐
│ Frontend│   "Connect Gmail"     │ Backend  │
│         ├──────────────────────>│ Gmail    │
│         │                        │Controller│
└─────────┘                        └────┬─────┘
                                        │
                                        │ 2. Generate OAuth URL
                                        ↓
                                   ┌─────────┐
                                   │ Gmail   │
                                   │ Service │
                                   └────┬────┘
     ┌───────────────────────────────── │
     │  3. Return OAuth URL             │
     ↓                                   │
┌─────────┐                              │
│Frontend │  4. Redirect to Google       │
└────┬────┘     OAuth screen             │
     │                                    │
     │  5. User grants permission         │
     │                                    │
     │  6. Redirect back with code        │
     └────────────────────────────────────┤
                                          │
     ┌────────────────────────────────────┘
     │  7. Send code to backend
     ↓
┌──────────┐     8. Exchange code    ┌─────────────┐
│ Backend  │      for tokens         │   Google    │
│ Gmail    ├────────────────────────>│   OAuth     │
│Controller│                          │   Server    │
└────┬─────┘                          └─────────────┘
     │
     │ 9. Store encrypted tokens
     ↓
┌────────────────┐    10. Save     ┌──────────────┐
│ Gmail Service  ├────────────────>│ Gmail Repo   │
└────────────────┘                  └──────┬───────┘
                                           │
                                           ↓
                                    ┌──────────────┐
                                    │   Database   │
                                    │GmailConnection│
                                    └──────────────┘
```

#### Flow 2: n8n Polls and Updates Application

```
┌──────────┐  1. Every 5 min      ┌──────────────┐
│   n8n    │  GET /active-        │   Backend    │
│ Workflow │  connections         │   Gmail      │
└────┬─────┘────────────────────> │  Controller  │
     │                             └──────┬───────┘
     │                                    │
     │  2. Returns list of users          │
     │     with tokens                    ↓
     │  <─────────────────────────  ┌──────────┐
     │                               │  Gmail   │
     │                               │ Service  │
     │                               └──────────┘
     │
     │  3. For each user:
     │     Fetch new emails
     ↓
┌──────────┐
│  Gmail   │
│   API    │
└────┬─────┘
     │
     │  4. Returns emails
     ↓
┌──────────┐  5. Parse & detect
│   n8n    │     status updates
│  Parse   │
│  Logic   │
└────┬─────┘
     │
     │  6. POST /webhook/email-update
     │     { userId, emailData, detectedStatus, ... }
     ↓
┌──────────────┐                    ┌────────────────┐
│   Backend    │  7. Process        │Email Processing│
│   Gmail      ├───────────────────>│    Service     │
│  Controller  │                    └────────┬───────┘
└──────────────┘                             │
                                             │ 8. Find matching
                                             │    application
                                             ↓
                                    ┌─────────────────┐
                                    │ Application     │
                                    │ Service/Repo    │
                                    └────────┬────────┘
                                             │
                                             │ 9. Update status/stage
                                             │    Add to history
                                             ↓
                                    ┌─────────────────┐
                                    │    Database     │
                                    │   Application   │
                                    │  StageHistory   │
                                    └─────────────────┘
```

---

### 4. Application Matching Strategy

**Challenge:** How do we match an email to a specific Application record?

**Proposed Algorithm (in EmailProcessingService):**

```csharp
public async Task<Application?> FindMatchingApplicationAsync(
    int userId, 
    string companyNameHint, 
    string emailContent)
{
    // Step 1: Get user's recent applications (last 90 days)
    var recentApplications = await _applicationRepository
        .GetRecentByUserIdAsync(userId, daysBack: 90);
    
    // Step 2: Try exact company name match first
    var exactMatch = recentApplications
        .FirstOrDefault(app => 
            app.UserCompany.Company.Name
                .Equals(companyNameHint, StringComparison.OrdinalIgnoreCase));
    
    if (exactMatch != null)
        return exactMatch;
    
    // Step 3: Try fuzzy company name match
    var fuzzyMatches = recentApplications
        .Where(app => 
            app.UserCompany.Company.Name
                .Contains(companyNameHint, StringComparison.OrdinalIgnoreCase) ||
            companyNameHint.Contains(app.UserCompany.Company.Name, StringComparison.OrdinalIgnoreCase))
        .ToList();
    
    if (fuzzyMatches.Count == 1)
        return fuzzyMatches.First();
    
    // Step 4: Try matching job title in email content
    if (fuzzyMatches.Count > 1)
    {
        foreach (var app in fuzzyMatches)
        {
            if (emailContent.Contains(app.JobTitle, StringComparison.OrdinalIgnoreCase))
                return app;
        }
        
        // If still multiple matches, return most recent
        return fuzzyMatches.OrderByDescending(a => a.SubmissionDate).First();
    }
    
    // Step 5: No match found - log for manual review
    return null;
}
```

---

### 5. Security Considerations

#### A. Token Encryption
**Problem:** OAuth tokens are sensitive and should not be stored in plain text

**Solution:** Implement encryption service

```csharp
// Services/Interfaces/IEncryptionService.cs
public interface IEncryptionService
{
    string Encrypt(string plainText);
    string Decrypt(string cipherText);
}

// Implementation uses AES with key from appsettings
// Key should be stored in environment variables or Azure Key Vault in production
```

#### B. n8n API Key Authentication
**Problem:** n8n endpoints should not be publicly accessible

**Solution:** Custom API key middleware

```csharp
// Add to Program.cs
builder.Services.AddScoped<N8nApiKeyAuthenticationHandler>();

// Use in controllers
[ServiceFilter(typeof(N8nApiKeyAuthenticationHandler))]
[HttpGet("active-connections")]
public async Task<IActionResult> GetActiveConnections()
```

#### C. Rate Limiting for Gmail Endpoints
```csharp
// Add specific rate limiting policy for Gmail endpoints
builder.Services.AddRateLimiter(options =>
{
    options.AddPolicy("GmailApiPolicy", context =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: context.Connection.RemoteIpAddress?.ToString(),
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 10,
                Window = TimeSpan.FromMinutes(1)
            }));
});
```

---

### 6. Database Schema Changes

**New Migration Required:**

```csharp
// Migration: AddGmailConnectionAndEmailUpdates

// Up():
- Create GmailConnections table
- Create EmailApplicationUpdates table
- Add foreign key: GmailConnections.UserId -> Users.UserId
- Add foreign key: EmailApplicationUpdates.ApplicationId -> Applications.ApplicationId
- Add foreign key: EmailApplicationUpdates.UserId -> Users.UserId
- Add unique index on GmailConnections.UserId
- Add unique index on GmailConnections.GmailAddress
- Add index on EmailApplicationUpdates.EmailId

// Down():
- Drop tables in reverse order
```

**Update to GPDBContext.cs:**
```csharp
public virtual DbSet<GmailConnection> GmailConnections { get; set; }
public virtual DbSet<EmailApplicationUpdate> EmailApplicationUpdates { get; set; }

// In OnModelCreating:
modelBuilder.Entity<GmailConnection>(entity =>
{
    entity.HasKey(e => e.GmailConnectionId);
    entity.HasIndex(e => e.UserId).IsUnique();
    entity.HasIndex(e => e.GmailAddress).IsUnique();
    entity.Property(e => e.Rowversion).IsRowVersion();
    
    entity.HasOne(d => d.User)
        .WithOne()
        .HasForeignKey<GmailConnection>(d => d.UserId)
        .OnDelete(DeleteBehavior.Cascade);
});
```

---

### 7. Configuration Updates

**appsettings.json additions:**
```json
{
  "GoogleOAuth": {
    "ClientId": "your-client-id",
    "ClientSecret": "your-client-secret",
    "RedirectUri": "https://yourfrontend.com/gmail/callback",
    "Scopes": "https://www.googleapis.com/auth/gmail.readonly"
  },
  "N8nSettings": {
    "ApiKey": "your-secure-api-key",
    "WebhookUrl": "https://your-n8n-instance.app.n8n.cloud/webhook/gmail"
  },
  "Encryption": {
    "Key": "your-256-bit-encryption-key-base64-encoded"
  }
}
```

---

### 8. Dependency Injection Registration

**Program.cs updates:**
```csharp
// Repositories
builder.Services.AddScoped<IGmailConnectionRepository, GmailConnectionRepository>();
builder.Services.AddScoped<IEmailApplicationUpdateRepository, EmailApplicationUpdateRepository>();

// Services
builder.Services.AddScoped<IGmailService, GmailService>();
builder.Services.AddScoped<IEmailProcessingService, EmailProcessingService>();
builder.Services.AddSingleton<IEncryptionService, EncryptionService>();

// HttpClient for Google OAuth
builder.Services.AddHttpClient<IGmailService, GmailService>();
```

---

## 🎯 Implementation Order (Recommended)

### Phase 1: Foundation
1. Create Models (GmailConnection, EmailApplicationUpdate)
2. Create DTOs for all operations
3. Create AutoMapper profiles
4. Create database migration
5. Create EncryptionService

### Phase 2: Repository Layer
1. Implement IGmailConnectionRepository
2. Implement IEmailApplicationUpdateRepository
3. Write unit tests for repositories

### Phase 3: Service Layer
1. Implement IGmailService (OAuth flow, token management)
2. Implement IEmailProcessingService (email matching, application updates)
3. Write unit tests for services

### Phase 4: Controller Layer
1. Create GmailController with all endpoints
2. Add API key authentication
3. Add rate limiting
4. Write integration tests

### Phase 5: Testing & Documentation
1. Test OAuth flow end-to-end
2. Test n8n webhook integration
3. Document setup process for developers
4. Create user documentation

---

## 📝 Open Questions for Discussion

1. **Encryption Key Management:** Where should we store the encryption key in production? (Azure Key Vault, AWS Secrets Manager, environment variable?)

2. **Email Matching Ambiguity:** What should happen when we can't confidently match an email to an application?
   - Log for manual review?
   - Notify user to manually link?
   - Store as "pending match"?

3. **Multiple Matches:** If an email could match multiple applications (e.g., same company, different positions), what's the priority?
   - Most recent application?
   - Prompt user to choose?

4. **Audit Trail:** Do we want EmailApplicationUpdate as a full entity, or just log to StageHistory?

5. **OAuth Token Refresh:** Should we proactively refresh tokens before expiry, or only when needed?

6. **User Notifications:** Should we notify users when their application status is auto-updated? (Out of scope for now, but good to plan)

---

## ✅ Architecture Benefits

1. **Clean Separation:** Each layer has a single responsibility
2. **Testability:** All layers can be unit tested independently
3. **Consistency:** Follows exact same pattern as existing features
4. **Security:** Tokens encrypted, API keys for n8n, rate limiting
5. **Scalability:** Repository pattern allows easy optimization
6. **Maintainability:** Clear boundaries and interfaces
7. **Extensibility:** Easy to add more email providers later (Outlook, Yahoo)

---

## 🚨 Potential Risks & Mitigations

| Risk | Mitigation |
|------|------------|
| Gmail API quota limits | Implement exponential backoff, cache recent checks |
| Token expiry during processing | Auto-refresh tokens before use |
| Incorrect application matching | Audit trail + manual review UI |
| n8n downtime | Backend can still function, just no auto-updates |
| Multiple OAuth sessions | Enforce one Gmail per user |
| Security breach of tokens | Encryption at rest, short token lifetimes |

---

## 📦 Deliverables Checklist

- [ ] Models created
- [ ] DTOs created
- [ ] Repositories implemented
- [ ] Services implemented
- [ ] Controller implemented
- [ ] Migration created and applied
- [ ] Encryption service implemented
- [ ] API key authentication implemented
- [ ] Unit tests written
- [ ] Integration tests written
- [ ] n8n workflow configured
- [ ] Documentation completed

---

**Ready to proceed?** Let me know if you want to discuss any aspect further, or we can start implementing phase by phase!

