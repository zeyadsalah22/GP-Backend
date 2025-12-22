using GPBackend.Models;
using GPBackend.Repositories.Implements;
using GPBackend.Repositories.Interfaces;
using GPBackend.Services.Implements;
using GPBackend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using GPBackend.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using GPBackend.Repositories;
using GPBackend.Services;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;
using System.Security.Claims;
using System.Text.Json.Serialization;
using GPBackend.Hubs;
using GPBackend.BackgoundServices;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Authorization;

namespace GPBackend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<GPDBContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddControllers();

            // Add CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials()
                          .WithOrigins("http://localhost:5253", "http://localhost:5173", "https://localhost:3000", "https://localhost:5253", "https://job-lander-frontend.vercel.app/");
                });
            });

            // Configure JWT Authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                    ValidAudience = builder.Configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"] ?? throw new InvalidOperationException("JWT Secret Key is not configured")))
                };

                // Add custom token validation to check the blacklist
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var tokenBlacklistService = context.HttpContext.RequestServices.GetRequiredService<ITokenBlacklistService>();
                        var token = context.SecurityToken as JwtSecurityToken;
                        var tokenString = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                        
                        if (tokenBlacklistService.IsBlacklisted(tokenString))
                        {
                            // If token is blacklisted, invalidate the authentication
                            context.Fail("Token has been revoked");
                        }
                        
                        return Task.CompletedTask;
                    },
                    
                    // CRITICAL: This enables JWT authentication for SignalR
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;
                        
                        // If the request is for our hub, get the token from query string
                        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/notificationhub"))
                        {
                            context.Token = accessToken;
                        }
                        
                        return Task.CompletedTask;
                    }
                };
            });

            // Configure Authorization Policies
            // Register HttpContextAccessor (required for ApiKeyAuthenticationHandler)
            builder.Services.AddHttpContextAccessor();
            
            // Register API Key authentication handler
            builder.Services.AddSingleton<IAuthorizationHandler, GPBackend.Authorization.ApiKeyAuthenticationHandler>();

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => 
                    policy.RequireRole("Admin"));
                
                options.AddPolicy("UserOrAdmin", policy => 
                    policy.RequireRole("User", "Admin"));
                
                // Policy that allows either JWT authentication OR n8n API Key
                options.AddPolicy("JwtOrApiKey", policy =>
                {
                    // Add the custom requirement - the handler will validate API key
                    policy.Requirements.Add(new GPBackend.Authorization.ApiKeyRequirement());
                });
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GP Backend API", Version = "v1" });

                // Configure Swagger to use JWT Authentication
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
                var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                {
                    c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
                }
            });

            builder.Services.AddSignalR();

            // Add AutoMapper
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Register repositories
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
            builder.Services.AddScoped<IUserCompanyRepository, UserCompanyRepository>();
            builder.Services.AddScoped<IResumeRepository, ResumeRepository>();
            builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();
            builder.Services.AddScoped<IInsightsRepository, InsightsRepository>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
            builder.Services.AddScoped<ITodoListRepository, TodoListRepository>();
            builder.Services.AddScoped<IInterviewRepository, InterviewRepository>();
            builder.Services.AddScoped<IInterviewQuestionRepository, InterviewQuestionRepository>();
            builder.Services.AddScoped<IResumeTestRepository, ResumeTestRepository>();
            builder.Services.AddScoped<ISkillRepository, SkillRepository>();
            builder.Services.AddScoped<IIndustryRepository, IndustryRepository>();
            builder.Services.AddScoped<IWeeklyGoalRepository, WeeklyGoalRepository>();
            builder.Services.AddScoped<IPostRepository, PostRepository>();
            builder.Services.AddScoped<ITagRepository, TagRepository>();
            builder.Services.AddScoped<IGmailConnectionRepository, GmailConnectionRepository>();
            builder.Services.AddScoped<IEmailApplicationUpdateRepository, EmailApplicationUpdateRepository>();
            builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
            builder.Services.AddScoped<INotificationPreferenceRepository, NotificationPreferenceRepository>();
            builder.Services.AddScoped<INotificationSignalRService, NotificationSignalRService>();
            builder.Services.AddScoped<IUserConnectionRepository, UserConnectionRepository>();
            builder.Services.AddScoped<ICommentRepository, CommentRepository>();
            builder.Services.AddScoped<IPostReactionRepository, PostReactionRepository>();
            builder.Services.AddScoped<ICommentReactionRepository, CommentReactionRepository>();
            builder.Services.AddScoped<ISavedPostRepository, SavedPostRepository>();
            builder.Services.AddScoped<ICommunityInterviewQuestionRepository, CommunityInterviewQuestionRepository>();
            builder.Services.AddScoped<IInterviewAnswerRepository, InterviewAnswerRepository>();
            builder.Services.AddScoped<IInterviewAnswerHelpfulRepository, InterviewAnswerHelpfulRepository>();
            builder.Services.AddScoped<IQuestionAskedByRepository, QuestionAskedByRepository>();

            // Register services
            builder.Services.AddScoped<IJwtService, JwtService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ICompanyService, CompanyService>();
            builder.Services.AddScoped<IUserCompanyService, UserCompanyService>();
            builder.Services.AddScoped<IResumeService, ResumeService>();
            builder.Services.AddScoped<IApplicationService, ApplicationService>();
            builder.Services.AddScoped<IInsightsService, InsightsService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IQuestionService, QuestionService>();
            builder.Services.AddScoped<ITodoListService, TodoListService>();
            builder.Services.AddScoped<IInterviewService, InterviewService>();
            builder.Services.AddScoped<IInterviewQuestionService, InterviewQuestionService>();
            builder.Services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            builder.Services.AddScoped<IResumeTestService, ResumeTestService>();
            builder.Services.AddScoped<IResumeTestMissingSkillsService, ResumeTestMissingSkillsService>();
            builder.Services.AddScoped<ISkillService, SkillService>();
            builder.Services.AddScoped<IPasswordResetTokenRepository, PasswordResetTokenRepository>();
            builder.Services.AddScoped<IPasswordResetService, PasswordResetService>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<IPostService, PostService>();
            builder.Services.AddScoped<ITagService, TagService>();

            builder.Services.AddScoped<INotificationService, NotificationService>();
            builder.Services.AddScoped<INotificationPreferenceService, NotificationPreferenceService>();
            builder.Services.AddScoped<ICommentService, CommentService>();
            builder.Services.AddScoped<IPostReactionService, PostReactionService>();
            builder.Services.AddScoped<ICommentReactionService, CommentReactionService>();
            builder.Services.AddScoped<ISavedPostService, SavedPostService>();
            builder.Services.AddScoped<ICommunityInterviewQuestionService, CommunityInterviewQuestionService>();
            builder.Services.AddScoped<IInterviewAnswerService, InterviewAnswerService>();

            // Register repositories
            builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            builder.Services.AddScoped<IIndustryService, IndustryService>();
            builder.Services.AddScoped<IWeeklyGoalService, WeeklyGoalService>();
            
            // Configure Data Protection API for secure encryption
            builder.Services.AddDataProtection()
                .SetApplicationName("GPBackend")  // Ensures keys work across app restarts
                .PersistKeysToFileSystem(new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "keys")));  // Store keys in app directory
            
            builder.Services.AddSingleton<IEncryptionService, EncryptionService>();
            builder.Services.AddScoped<IGmailService, GmailService>();
            builder.Services.AddScoped<IEmailProcessingService, EmailProcessingService>();
            
            // Register Gmail Watch Renewal Background Service
            builder.Services.AddHostedService<GmailWatchRenewalService>();
            
            // builder.Services.AddHttpClient<IInterviewService, InterviewService>();
            
            // Register HttpClient for GmailService
            builder.Services.AddHttpClient<IGmailService, GmailService>();

            builder.Services.AddControllers()
                .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            // Register TokenBlacklistService as Singleton (persistence across requests)
            builder.Services.AddSingleton<ITokenBlacklistService, TokenBlacklistService>();
            
            // Register HttpClient for ChatBotController
            builder.Services.AddHttpClient();

            // Register HttpClient for MLServiceClient with 5-minute timeout
            builder.Services.AddHttpClient<IMLServiceClient, MLServiceClient>(client =>
            {
                var baseUrl = builder.Configuration["MLService:BaseUrl"] ?? throw new InvalidOperationException("MLService:BaseUrl is not configured");
                client.BaseAddress = new Uri(baseUrl);
                client.Timeout = TimeSpan.FromMinutes(5);
            });

            // Register background services
            builder.Services.AddHostedService<TokenCleanupService>();
            builder.Services.AddHostedService<NotificationTriggeringService>();

            builder.Services.AddRateLimiter(options => {
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                // Default: per-IP global limit
                options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
                {
                    var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
                    return RateLimitPartition.GetFixedWindowLimiter(ip, _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 120, // 120 req/min per IP
                        Window = TimeSpan.FromMinutes(1),
                        QueueLimit = 0
                    });
                });

                // Add a custom limiter for the /api/chatbot endpoint
                options.AddPolicy("ChatbotPerIp", context =>
                {
                    var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
                    return RateLimitPartition.GetFixedWindowLimiter(ip, _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 10,
                        Window = TimeSpan.FromMinutes(1),
                        QueueLimit = 0
                    });
                });

                // Add a custom limiter for forgot-password, reset-password, register and verify-email endpoints
                options.AddPolicy("CustomUserLimiter", context =>
                {
                    var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
                    return RateLimitPartition.GetFixedWindowLimiter(ip, _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 3, // 3 req/min per IP
                        Window = TimeSpan.FromMinutes(1),
                        QueueLimit = 0
                    });
                });

                // Rate limiter for Gmail API endpoints (n8n polling)
                options.AddPolicy("GmailApiLimiter", context =>
                {
                    var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
                    return RateLimitPartition.GetFixedWindowLimiter(ip, _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 30, // 30 req/min per IP
                        Window = TimeSpan.FromMinutes(1),
                        QueueLimit = 0
                    });
                });

                // Per-user policy for authenticated endpoints
                options.AddPolicy("PerUser", ctx =>
                {
                    var userId = ctx.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "anon";
                    return RateLimitPartition.GetSlidingWindowLimiter(userId, _ => new SlidingWindowRateLimiterOptions
                    {
                        PermitLimit = 60, // 60 requests / 1 min per user
                        Window = TimeSpan.FromMinutes(1),
                        SegmentsPerWindow = 6,
                        QueueLimit = 0
                    });
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Use CORS
            app.UseCors("AllowAll");

            // Must be called before UseAuthorization
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseRateLimiter();

            app.MapControllers();

            app.MapHub<NotificationHub>("/notificationhub");
            app.Run();
        }
    }
}
