using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GPBackend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Industries",
                columns: table => new
                {
                    industry_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    rowversion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Industries", x => x.industry_id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    lname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    birth_date = table.Column<DateOnly>(type: "date", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    rowversion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    role = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    company_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    location = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    careers_link = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    linkedin_link = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    industry_id = table.Column<int>(type: "int", nullable: false),
                    company_size = table.Column<int>(type: "int", nullable: false),
                    logo = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    rowversion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.company_id);
                    table.ForeignKey(
                        name: "FK_Companies_Industries",
                        column: x => x.industry_id,
                        principalTable: "Industries",
                        principalColumn: "industry_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PasswordResetTokens",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    token_hash = table.Column<byte[]>(type: "varbinary(32)", maxLength: 32, nullable: false),
                    expires_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false),
                    used_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    created_ip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_user_agent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysutcdatetime())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordResetTokens", x => x.id);
                    table.ForeignKey(
                        name: "FK_PasswordResetTokens_Users",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    refresh_token_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    token = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    expiry_date = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false),
                    is_revoked = table.Column<bool>(type: "bit", nullable: false),
                    is_used = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    replaced_by_token = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.refresh_token_id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Resumes",
                columns: table => new
                {
                    resume_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    resume_file = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysdatetime())"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysdatetime())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resumes", x => x.resume_id);
                    table.ForeignKey(
                        name: "FK_Resumes_Users",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TodoLists",
                columns: table => new
                {
                    todo_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    application_title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    application_link = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    deadline = table.Column<DateTime>(type: "datetime2", nullable: true),
                    completed = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysdatetime())"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysdatetime())"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoLists", x => new { x.todo_id, x.user_id });
                    table.ForeignKey(
                        name: "FK_TodoLists_Users",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WeeklyGoals",
                columns: table => new
                {
                    weekly_goal_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    week_start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    week_end_date = table.Column<DateOnly>(type: "date", nullable: false),
                    target_application_count = table.Column<int>(type: "int", nullable: false),
                    notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    rowversion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeeklyGoals", x => x.weekly_goal_id);
                    table.ForeignKey(
                        name: "FK_WeeklyGoals_Users",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User_Companies",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    personal_notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    interest_level = table.Column<int>(type: "int", nullable: true),
                    favorite = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysdatetime())"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysdatetime())"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Companies", x => new { x.user_id, x.company_id });
                    table.ForeignKey(
                        name: "FK_User_Companies_Companies",
                        column: x => x.company_id,
                        principalTable: "Companies",
                        principalColumn: "company_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Companies_Users",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResumeTests",
                columns: table => new
                {
                    test_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    resume_id = table.Column<int>(type: "int", nullable: false),
                    ats_score = table.Column<int>(type: "int", nullable: false),
                    test_date = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    job_description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResumeTests", x => x.test_id);
                    table.ForeignKey(
                        name: "FK_ResumeTests_Resumes",
                        column: x => x.resume_id,
                        principalTable: "Resumes",
                        principalColumn: "resume_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    application_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    job_title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    job_type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    link = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    submitted_cv_id = table.Column<int>(type: "int", nullable: true),
                    ats_score = table.Column<int>(type: "int", nullable: true),
                    stage = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    submission_date = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "(CONVERT([date],getdate()))"),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysdatetime())"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysdatetime())"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    rowversion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.application_id);
                    table.ForeignKey(
                        name: "FK_Applications_Resumes",
                        column: x => x.submitted_cv_id,
                        principalTable: "Resumes",
                        principalColumn: "resume_id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Applications_User_Companies",
                        columns: x => new { x.user_id, x.company_id },
                        principalTable: "User_Companies",
                        principalColumns: new[] { "user_id", "company_id" });
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    employee_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    linkedin_link = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    job_title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    contacted = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    department = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.employee_id);
                    table.ForeignKey(
                        name: "FK_Employees_User_Companies",
                        columns: x => new { x.user_id, x.company_id },
                        principalTable: "User_Companies",
                        principalColumns: new[] { "user_id", "company_id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserCompany_Tags",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    tag = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysdatetime())"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysdatetime())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCompany_Tags", x => x.id);
                    table.ForeignKey(
                        name: "FK_UserCompanyTags_UserCompanies",
                        columns: x => new { x.user_id, x.company_id },
                        principalTable: "User_Companies",
                        principalColumns: new[] { "user_id", "company_id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    skill_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    test_id = table.Column<int>(type: "int", nullable: false),
                    skill = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.skill_id);
                    table.ForeignKey(
                        name: "FK_Skills_ResumeTests",
                        column: x => x.test_id,
                        principalTable: "ResumeTests",
                        principalColumn: "test_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Application_Stage_History",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    application_id = table.Column<int>(type: "int", nullable: false),
                    stage = table.Column<int>(type: "int", nullable: false),
                    reached_date = table.Column<DateOnly>(type: "date", nullable: false),
                    note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysdatetime())"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysdatetime())"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    rowversion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Application_Stage_History", x => x.id);
                    table.ForeignKey(
                        name: "FK_AppStageHistory_Applications",
                        column: x => x.application_id,
                        principalTable: "Applications",
                        principalColumn: "application_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Interviews",
                columns: table => new
                {
                    interview_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    application_id = table.Column<int>(type: "int", nullable: true),
                    company_id = table.Column<int>(type: "int", nullable: true),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    position = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    JobDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    feedback = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    start_date = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false),
                    duration = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interviews", x => x.interview_id);
                    table.ForeignKey(
                        name: "FK_Interviews_Applications",
                        column: x => x.application_id,
                        principalTable: "Applications",
                        principalColumn: "application_id");
                    table.ForeignKey(
                        name: "FK_Interviews_Companies",
                        column: x => x.company_id,
                        principalTable: "Companies",
                        principalColumn: "company_id");
                    table.ForeignKey(
                        name: "FK_Interviews_Users",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    question_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    answer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    application_id = table.Column<int>(type: "int", nullable: false),
                    type = table.Column<int>(type: "int", nullable: true),
                    answer_status = table.Column<int>(type: "int", nullable: true),
                    difficulty = table.Column<int>(type: "int", nullable: true),
                    preparation_note = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    favorite = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.question_id);
                    table.ForeignKey(
                        name: "FK_Questions_Applications",
                        column: x => x.application_id,
                        principalTable: "Applications",
                        principalColumn: "application_id");
                });

            migrationBuilder.CreateTable(
                name: "ApplicationEmployee",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    application_id = table.Column<int>(type: "int", nullable: false),
                    employee_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationEmployee", x => x.id);
                    table.ForeignKey(
                        name: "FK_AppEmployee_Employees",
                        column: x => x.employee_id,
                        principalTable: "Employees",
                        principalColumn: "employee_id");
                    table.ForeignKey(
                        name: "FK_ApplicationEmployee_Applications",
                        column: x => x.application_id,
                        principalTable: "Applications",
                        principalColumn: "application_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Interview_Questions",
                columns: table => new
                {
                    question_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    interview_id = table.Column<int>(type: "int", nullable: false),
                    question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    answer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interview_Questions", x => x.question_id);
                    table.ForeignKey(
                        name: "FK_Interview_Questions_Interviews",
                        column: x => x.interview_id,
                        principalTable: "Interviews",
                        principalColumn: "interview_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Question_Tags",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    question_id = table.Column<int>(type: "int", nullable: false),
                    tag = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysdatetime())"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysdatetime())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question_Tags", x => x.id);
                    table.ForeignKey(
                        name: "FK_QuestionTags_Questions",
                        column: x => x.question_id,
                        principalTable: "Questions",
                        principalColumn: "question_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Application_Stage_History_application_id_stage",
                table: "Application_Stage_History",
                columns: new[] { "application_id", "stage" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppEmployee_App",
                table: "ApplicationEmployee",
                column: "application_id");

            migrationBuilder.CreateIndex(
                name: "IX_AppEmployee_Emp",
                table: "ApplicationEmployee",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "UQ_ApplicationEmployee_UniquePair",
                table: "ApplicationEmployee",
                columns: new[] { "application_id", "employee_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Applications_SubmittedCvId",
                table: "Applications",
                column: "submitted_cv_id");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_user_id_company_id",
                table: "Applications",
                columns: new[] { "user_id", "company_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_industry_id",
                table: "Companies",
                column: "industry_id");

            migrationBuilder.CreateIndex(
                name: "UQ_Companies_Name",
                table: "Companies",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_user_id_company_id",
                table: "Employees",
                columns: new[] { "user_id", "company_id" });

            migrationBuilder.CreateIndex(
                name: "UQ_Industries_Name",
                table: "Industries",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Interview_Questions_interview_id",
                table: "Interview_Questions",
                column: "interview_id");

            migrationBuilder.CreateIndex(
                name: "IX_Interviews_application_id",
                table: "Interviews",
                column: "application_id");

            migrationBuilder.CreateIndex(
                name: "IX_Interviews_company_id",
                table: "Interviews",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_Interviews_user_id",
                table: "Interviews",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_PasswordResetTokens_token_hash",
                table: "PasswordResetTokens",
                column: "token_hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PasswordResetTokens_user_id",
                table: "PasswordResetTokens",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Question_Tags_question_id_tag",
                table: "Question_Tags",
                columns: new[] { "question_id", "tag" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_application_id",
                table: "Questions",
                column: "application_id");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_user_id",
                table: "RefreshTokens",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Resumes_user_id",
                table: "Resumes",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_ResumeTests_resume_id",
                table: "ResumeTests",
                column: "resume_id");

            migrationBuilder.CreateIndex(
                name: "UQ_Skills_TestSkill",
                table: "Skills",
                columns: new[] { "test_id", "skill" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TodoLists_user_id",
                table: "TodoLists",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_User_Companies_company_id",
                table: "User_Companies",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_UserCompany_Tags_user_id_company_id_tag",
                table: "UserCompany_Tags",
                columns: new[] { "user_id", "company_id", "tag" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Users_Email",
                table: "Users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WeeklyGoals_user_id_week_start_date",
                table: "WeeklyGoals",
                columns: new[] { "user_id", "week_start_date" },
                unique: true,
                filter: "[is_deleted] = 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Application_Stage_History");

            migrationBuilder.DropTable(
                name: "ApplicationEmployee");

            migrationBuilder.DropTable(
                name: "Interview_Questions");

            migrationBuilder.DropTable(
                name: "PasswordResetTokens");

            migrationBuilder.DropTable(
                name: "Question_Tags");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "TodoLists");

            migrationBuilder.DropTable(
                name: "UserCompany_Tags");

            migrationBuilder.DropTable(
                name: "WeeklyGoals");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Interviews");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "ResumeTests");

            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "Resumes");

            migrationBuilder.DropTable(
                name: "User_Companies");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Industries");
        }
    }
}
