using System.Text;
using AIStudyHub.API.Middlewares;
using AIStudyHub.BLL.Interfaces;
using AIStudyHub.BLL.Services;
using AIStudyHub.DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// ═══════════════════════════════════════════════════════════════
// 1. CONTROLLERS & JSON
// ═══════════════════════════════════════════════════════════════
builder.Services.AddControllers()
    .AddJsonOptions(opts =>
    {
        opts.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
        opts.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });

// ═══════════════════════════════════════════════════════════════
// 2. SWAGGER / OPENAPI
// ═══════════════════════════════════════════════════════════════
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "AI Study Hub API",
        Version = "v1",
        Description = "AI-Powered Learning Document Management System – Backend API",
        Contact = new OpenApiContact
        {
            Name = "SE1923 Group 5",
            Email = "group5@fpt.edu.vn"
        }
    });

    // JWT Bearer auth in Swagger UI
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT token: Bearer {token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
});

// ═══════════════════════════════════════════════════════════════
// 3. JWT AUTHENTICATION
// ═══════════════════════════════════════════════════════════════
var jwtSection = builder.Configuration.GetSection("Jwt");
var secretKey = jwtSection["SecretKey"] ?? "YourSuperSecretKeyThatIsAtLeast32Chars!";

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
        ValidIssuer = jwtSection["Issuer"],
        ValidAudience = jwtSection["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

// ═══════════════════════════════════════════════════════════════
// 4. CORS
// ═══════════════════════════════════════════════════════════════
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
                builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>()
                ?? new[] { "http://localhost:3000", "http://localhost:5173" })
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

// ═══════════════════════════════════════════════════════════════
// 5. DATABASE CONTEXT
// ═══════════════════════════════════════════════════════════════
// TODO: Uncomment after running Scaffold-DbContext
// builder.Services.AddDbContext<AppDbContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ═══════════════════════════════════════════════════════════════
// 6. DEPENDENCY INJECTION – Repositories
// ═══════════════════════════════════════════════════════════════
// TODO: Register repositories after scaffolding
// builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
builder.Services.AddScoped<IChatSessionRepository, ChatSessionRepository>();

// ═══════════════════════════════════════════════════════════════
// 7. DEPENDENCY INJECTION – Services
// ═══════════════════════════════════════════════════════════════
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IDocumentService, DocumentService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<IChatService, ChatService>();

// ═══════════════════════════════════════════════════════════════
// BUILD & CONFIGURE PIPELINE
// ═══════════════════════════════════════════════════════════════
var app = builder.Build();

// Global exception handling
app.UseMiddleware<GlobalExceptionMiddleware>();

// Swagger (development only)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AI Study Hub API v1");
        c.RoutePrefix = string.Empty; // Swagger at root URL
    });
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
