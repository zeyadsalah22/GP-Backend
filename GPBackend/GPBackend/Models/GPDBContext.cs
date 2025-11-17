using GPBackend.Data;
using GPBackend.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace GPBackend.Models;

public partial class GPDBContext : DbContext
{
    public GPDBContext()
    {
    }

    public GPDBContext(DbContextOptions<GPDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<ApplicationEmployee> ApplicationEmployees { get; set; }
    public virtual DbSet<ApplicationStageHistory> ApplicationStageHistories { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Industry> Industries { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Interview> Interviews { get; set; }

    public virtual DbSet<InterviewQuestion> InterviewQuestions { get; set; }

    public virtual DbSet<Question> Questions { get; set; }
    public virtual DbSet<QuestionTag> QuestionTags { get; set; }

    public virtual DbSet<Resume> Resumes { get; set; }

    public virtual DbSet<ResumeTest> ResumeTests { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<TodoList> TodoLists { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserCompany> UserCompanies { get; set; }
    public virtual DbSet<UserCompanyTag> UserCompanyTags { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<PasswordResetToken> PasswordResetTokens { get; set; }

    public virtual DbSet<WeeklyGoal> WeeklyGoals { get; set; }

    public virtual DbSet<GmailConnection> GmailConnections { get; set; }

    public virtual DbSet<EmailApplicationUpdate> EmailApplicationUpdates { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }
    public virtual DbSet<UserConnection> UserConnections { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Application>(entity =>
        {
            entity.HasIndex(e => e.SubmittedCvId, "IX_Applications_SubmittedCvId");

            entity.Property(e => e.ApplicationId).HasColumnName("application_id");
            entity.Property(e => e.AtsScore).HasColumnName("ats_score");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.JobTitle)
                .HasMaxLength(100)
                .HasColumnName("job_title");
            entity.Property(e => e.JobType)
                .HasMaxLength(50)
                .HasColumnName("job_type");
            entity.Property(e => e.Link)
                .HasMaxLength(255)
                .HasColumnName("link");
            entity.Property(e => e.Rowversion)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("rowversion");
            entity.Property(e => e.Stage)
                .HasConversion<int>()
                .HasColumnName("stage");
            entity.Property(e => e.Status)
                .HasConversion<int>()
                .HasColumnName("status");
            entity.Property(e => e.SubmissionDate)
                .HasDefaultValueSql("(CONVERT([date],getdate()))")
                .HasColumnName("submission_date");
            entity.Property(e => e.SubmittedCvId).HasColumnName("submitted_cv_id");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.SubmittedCv).WithMany(p => p.Applications)
                .HasForeignKey(d => d.SubmittedCvId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Applications_Resumes");

            entity.HasOne(d => d.UserCompany).WithMany(p => p.Applications)
                .HasForeignKey(d => new { d.UserId, d.CompanyId })
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_Applications_User_Companies");
        });

        modelBuilder.Entity<ApplicationStageHistory>(entity =>
        {
            entity.ToTable("Application_Stage_History");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ApplicationId).HasColumnName("application_id");
            entity.Property(e => e.Stage)
                .HasConversion<int>()
                .HasColumnName("stage");
            entity.Property(e => e.ReachedDate).HasColumnName("reached_date");
            entity.Property(e => e.Note).HasColumnName("note");
            entity.Property(e => e.CreatedAt).HasPrecision(0).HasDefaultValueSql("(sysdatetime())").HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt).HasPrecision(0).HasDefaultValueSql("(sysdatetime())").HasColumnName("updated_at");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.Rowversion).IsRowVersion().IsConcurrencyToken().HasColumnName("rowversion");

            entity.HasIndex(e => new { e.ApplicationId, e.Stage }).IsUnique();

            entity.HasOne(d => d.Application)
                .WithMany(p => p.StageHistory)
                .HasForeignKey(d => d.ApplicationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_AppStageHistory_Applications");
        });

        modelBuilder.Entity<ApplicationEmployee>(entity =>
        {
            entity.ToTable("ApplicationEmployee");

            entity.HasIndex(e => e.ApplicationId, "IX_AppEmployee_App");

            entity.HasIndex(e => e.EmployeeId, "IX_AppEmployee_Emp");

            entity.HasIndex(e => new { e.ApplicationId, e.EmployeeId }, "UQ_ApplicationEmployee_UniquePair").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ApplicationId).HasColumnName("application_id");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

            entity.HasOne(d => d.Application).WithMany(p => p.ApplicationEmployees)
                .HasForeignKey(d => d.ApplicationId)
                .HasConstraintName("FK_ApplicationEmployee_Applications");

            entity.HasOne(d => d.Employee).WithMany(p => p.ApplicationEmployees)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AppEmployee_Employees");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasIndex(e => e.Name, "UQ_Companies_Name").IsUnique();

            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.CareersLink)
                .HasMaxLength(255)
                .HasColumnName("careers_link");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.LinkedinLink)
                .HasMaxLength(255)
                .HasColumnName("linkedin_link");
            entity.Property(e => e.IndustryId).HasColumnName("industry_id");
            entity.Property(e => e.CompanySize)
                .HasMaxLength(255)
                .HasColumnName("company_size");
            entity.Property(e => e.LogoUrl).HasColumnName("logo");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .HasColumnName("location");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Rowversion)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("rowversion");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Industry)
                .WithMany(p => p.Companies)
                .HasForeignKey(d => d.IndustryId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Companies_Industries");

            entity.HasData(CompanySeedData.GetCompanies());
        });

        modelBuilder.Entity<Industry>(entity =>
        {
            entity.HasIndex(e => e.Name, "UQ_Industries_Name").IsUnique();

            entity.Property(e => e.IndustryId).HasColumnName("industry_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("updated_at");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.Rowversion)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("rowversion");
            
            entity.HasData(IndustrySeedData.GetIndustries());
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.Contacted)
                .HasMaxLength(255)
                .HasColumnName("contacted");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasColumnName("phone");
            entity.Property(e => e.Department)
                .HasMaxLength(100)
                .HasColumnName("department");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.JobTitle)
                .HasMaxLength(100)
                .HasColumnName("job_title");
            entity.Property(e => e.LinkedinLink)
                .HasMaxLength(255)
                .HasColumnName("linkedin_link");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.UserCompany).WithMany(p => p.Employees)
                .HasForeignKey(d => new { d.UserId, d.CompanyId })
                .HasConstraintName("FK_Employees_User_Companies");
        });

        modelBuilder.Entity<Interview>(entity =>
        {
            entity.Property(e => e.InterviewId).HasColumnName("interview_id");
            entity.Property(e => e.ApplicationId).HasColumnName("application_id");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.Feedback).HasColumnName("feedback");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.Position)
                .HasMaxLength(100)
                .HasColumnName("position");
            entity.Property(e => e.StartDate)
                .HasPrecision(0)
                .HasColumnName("start_date");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Application).WithMany(p => p.Interviews)
                .HasForeignKey(d => d.ApplicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Interviews_Applications");

            entity.HasOne(d => d.Company).WithMany(p => p.Interviews)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Interviews_Companies");

            entity.HasOne(d => d.User).WithMany(p => p.Interviews)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Interviews_Users");
        });

        modelBuilder.Entity<InterviewQuestion>(entity =>
        {
            entity.HasKey(e => e.QuestionId);

            entity.ToTable("Interview_Questions");

            entity.Property(e => e.QuestionId).HasColumnName("question_id");
            entity.Property(e => e.Answer).HasColumnName("answer");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");
            entity.Property(e => e.InterviewId).HasColumnName("interview_id");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.Question).HasColumnName("question");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Interview).WithMany(p => p.InterviewQuestions)
                .HasForeignKey(d => d.InterviewId)
                .HasConstraintName("FK_Interview_Questions_Interviews");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.Property(e => e.QuestionId).HasColumnName("question_id");
            entity.Property(e => e.Answer).HasColumnName("answer");
            entity.Property(e => e.ApplicationId).HasColumnName("application_id");
            entity.Property(e => e.Type)
                .HasConversion<int>()
                .HasColumnName("type");
            entity.Property(e => e.AnswerStatus)
                .HasConversion<int>()
                .HasColumnName("answer_status");
            entity.Property(e => e.Difficulty).HasColumnName("difficulty");
            entity.Property(e => e.PreparationNote)
                .HasMaxLength(1000)
                .HasColumnName("preparation_note");
            entity.Property(e => e.Favorite).HasColumnName("favorite");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.Question1).HasColumnName("question");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Application).WithMany(p => p.Questions)
                .HasForeignKey(d => d.ApplicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Questions_Applications");
        });

        modelBuilder.Entity<QuestionTag>(entity =>
        {
            entity.ToTable("Question_Tags");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.QuestionId).HasColumnName("question_id");
            entity.Property(e => e.Tag).HasMaxLength(50).HasColumnName("tag");
            entity.Property(e => e.CreatedAt).HasPrecision(0).HasDefaultValueSql("(sysdatetime())").HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt).HasPrecision(0).HasDefaultValueSql("(sysdatetime())").HasColumnName("updated_at");

            entity.HasIndex(e => new { e.QuestionId, e.Tag }).IsUnique();

            entity.HasOne(d => d.Question)
                .WithMany(p => p.Tags)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_QuestionTags_Questions");
        });

        modelBuilder.Entity<Resume>(entity =>
        {
            entity.Property(e => e.ResumeId).HasColumnName("resume_id");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("created_at");
            entity.Property(e => e.ResumeFile).HasColumnName("resume_file");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Resumes)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Resumes_Users");
        });

        modelBuilder.Entity<ResumeTest>(entity =>
        {
            entity.HasKey(e => e.TestId);

            entity.Property(e => e.TestId).HasColumnName("test_id");
            entity.Property(e => e.AtsScore).HasColumnName("ats_score");
            entity.Property(e => e.JobDescription)
                .HasMaxLength(1000)
                .HasColumnName("job_description");
            entity.Property(e => e.ResumeId).HasColumnName("resume_id");
            entity.Property(e => e.TestDate)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("test_date");

            entity.HasOne(d => d.Resume).WithMany(p => p.ResumeTests)
                .HasForeignKey(d => d.ResumeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ResumeTests_Resumes");
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasIndex(e => new { e.TestId, e.Skill1 }, "UQ_Skills_TestSkill").IsUnique();

            entity.Property(e => e.SkillId).HasColumnName("skill_id");
            entity.Property(e => e.Skill1)
                .HasMaxLength(100)
                .HasColumnName("skill");
            entity.Property(e => e.TestId).HasColumnName("test_id");

            entity.HasOne(d => d.Test).WithMany(p => p.Skills)
                .HasForeignKey(d => d.TestId)
                .HasConstraintName("FK_Skills_ResumeTests");
        });

        modelBuilder.Entity<TodoList>(entity =>
        {
            entity.HasKey(e => new { e.TodoId, e.UserId });

            entity.Property(e => e.TodoId)
                .ValueGeneratedOnAdd()
                .HasColumnName("todo_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.ApplicationLink)
                .HasMaxLength(255)
                .HasColumnName("application_link");
            entity.Property(e => e.ApplicationTitle)
                .HasMaxLength(100)
                .HasColumnName("application_title");
            entity.Property(e => e.Completed).HasColumnName("completed");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("created_at");
            entity.Property(e => e.Deadline).HasColumnName("deadline");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.User).WithMany(p => p.TodoLists)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_TodoLists_Users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Email, "UQ_Users_Email").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Fname)
                .HasMaxLength(50)
                .HasColumnName("fname");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.Lname)
                .HasMaxLength(50)
                .HasColumnName("lname");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasConversion<int>()
                .HasDefaultValue(UserRole.User)
                .HasColumnName("role");
            entity.Property(e => e.Rowversion)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("rowversion");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("updated_at");

            entity.HasData(UserSeedData.GetAdminUsers());
        });

        modelBuilder.Entity<UserCompany>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.CompanyId });

            entity.ToTable("User_Companies");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("created_at");
            entity.Property(e => e.PersonalNotes).HasColumnName("personal_notes");
            entity.Property(e => e.InterestLevel)
                .HasConversion<int>()
                .HasColumnName("interest_level");
            entity.Property(e => e.Favorite).HasColumnName("favorite");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Company).WithMany(p => p.UserCompanies)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK_User_Companies_Companies");

            entity.HasOne(d => d.User).WithMany(p => p.UserCompanies)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_User_Companies_Users");
        });

        modelBuilder.Entity<UserCompanyTag>(entity =>
        {
            entity.ToTable("UserCompany_Tags");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.Tag).HasMaxLength(50).HasColumnName("tag");
            entity.Property(e => e.CreatedAt).HasPrecision(0).HasDefaultValueSql("(sysdatetime())").HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt).HasPrecision(0).HasDefaultValueSql("(sysdatetime())").HasColumnName("updated_at");

            entity.HasIndex(e => new { e.UserId, e.CompanyId, e.Tag }).IsUnique();

            entity.HasOne(e => e.UserCompany)
                .WithMany(uc => uc.Tags)
                .HasForeignKey(e => new { e.UserId, e.CompanyId })
                .HasConstraintName("FK_UserCompanyTags_UserCompanies");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.Property(e => e.RefreshTokenId).HasColumnName("refresh_token_id");
            entity.Property(e => e.Token)
                .HasMaxLength(500)
                .HasColumnName("token");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.ExpiryDate)
                .HasPrecision(0)
                .HasColumnName("expiry_date");
            entity.Property(e => e.IsRevoked).HasColumnName("is_revoked");
            entity.Property(e => e.IsUsed).HasColumnName("is_used");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");
            entity.Property(e => e.ReplacedByToken)
                .HasMaxLength(500)
                .HasColumnName("replaced_by_token");

            entity.HasOne(d => d.User).WithMany(p => p.RefreshTokens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_RefreshTokens_Users");
        });

        modelBuilder.Entity<PasswordResetToken>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.TokenHash)
                .HasMaxLength(32)
                .IsRequired()
                .HasColumnName("token_hash");
            entity.HasIndex(e => e.TokenHash).IsUnique();
            entity.Property(e => e.ExpiresAt)
                .HasPrecision(0)
                .HasColumnName("expires_at");
            entity.Property(e => e.UsedAt)
                .HasPrecision(0)
                .HasColumnName("used_at");
            entity.Property(e => e.CreatedIp)
                .HasColumnName("created_ip");
            entity.Property(e => e.CreatedUserAgent)
                .HasColumnName("created_user_agent");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");

            entity.HasOne(d => d.User)
                .WithMany(p => p.PasswordResetTokens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_PasswordResetTokens_Users");
        });

        modelBuilder.Entity<WeeklyGoal>(entity =>
        {
            entity.HasKey(e => e.WeeklyGoalId);

            entity.Property(e => e.WeeklyGoalId).HasColumnName("weekly_goal_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.WeekStartDate).HasColumnName("week_start_date");
            entity.Property(e => e.WeekEndDate).HasColumnName("week_end_date");
            entity.Property(e => e.TargetApplicationCount).HasColumnName("target_application_count");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("updated_at");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.Rowversion)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("rowversion");

            // Unique constraint: one goal per user per week start date
            entity.HasIndex(e => new { e.UserId, e.WeekStartDate })
                .IsUnique()
                .HasFilter("[is_deleted] = 0");

            entity.HasOne(d => d.User)
                .WithMany(p => p.WeeklyGoals)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_WeeklyGoals_Users");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.Property(e => e.NotificationId).HasColumnName("notification_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.ActorId).HasColumnName("actor_id");
            entity.Property(e => e.Type)
                .HasConversion<int>()
                .HasColumnName("type");
            entity.Property(e => e.EntityTargetedId).HasColumnName("entity_targeted_id");
            entity.Property(e => e.Message)
                .HasColumnName("message");
            entity.Property(e => e.IsRead).HasColumnName("is_read");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Notifications_Users");

            entity.HasOne(d => d.Actor).WithMany()
                .HasForeignKey(d => d.ActorId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_Notifications_Actor");
        });

        modelBuilder.Entity<UserConnection>(entity =>
        {
            // Composite Key
            entity.HasKey(uc => new {uc.UserId, uc.ConnectionId});
            
            entity.Property(uc => uc.UserId).HasColumnName("user_id");
            entity.Property(uc => uc.ConnectionId)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnName("connection_id");
            entity.Property(uc => uc.ConnectedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("connected_at");
            
            // One-to-one relationship
            entity.HasOne(uc => uc.User)
                .WithOne(u => u.UserConnection)
                .HasForeignKey<UserConnection>(uc => uc.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_UserConnections_Users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

}
