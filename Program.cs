using FluentValidation;
using FluentValidation.AspNetCore;
using GACHSLApi.Configurations;
using GACHSLApi.Data;
using GACHSLApi.Interfaces;
using GACHSLApi.Mappings;
using GACHSLApi.Middleware;
using GACHSLApi.Models;
using GACHSLApi.Repositories;
using GACHSLApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;


Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(
        "Logs/log-.txt",
        rollingInterval: RollingInterval.Day)
    .CreateLogger();


var builder = WebApplication.CreateBuilder(args);


builder.Host.UseSerilog();


// =======================
// Controllers
// =======================
builder.Services.AddControllers();


// =======================
// CORS
// =======================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});


// =======================
// AutoMapper
// =======================
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAutoMapper(typeof(MappingProfile));


// =======================
// Fluent Validation
// =======================
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();


// =======================
// JWT Authentication
// =======================
builder.Services.AddAuthentication(
    JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer =
                    builder.Configuration["Jwt:Issuer"],

                ValidAudience =
                    builder.Configuration["Jwt:Audience"],

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            builder.Configuration["Jwt:Key"]!
                        ))
            };
    });

builder.Services.AddAuthorization();
// =======================
// Database
// =======================
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration
            .GetConnectionString("DefaultConnection"),

        ServerVersion.AutoDetect(
            builder.Configuration
                .GetConnectionString("DefaultConnection"))
    ));


// =======================
// Repositories
// =======================
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<IPasswordResetOtpRepository, PasswordResetOtpRepository>();
builder.Services.AddScoped<IPasswordResetTokenRepository, PasswordResetTokenRepository>();

builder.Services.AddScoped<INoticeRepository, NoticeRepository>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<ISocietySettingsRepository, SocietySettingsRepository>();
builder.Services.AddScoped<IMeetingRepository, MeetingRepository>();
builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
builder.Services.AddScoped<IMeetingDocumentRepository, MeetingDocumentRepository>();
builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();
builder.Services.AddScoped<IMeetingDocumentService, MeetingDocumentService>();

// =======================
// Services
// =======================
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMemberService, MemberService>();

builder.Services.AddScoped<IMeetingService, MeetingService>();
builder.Services.AddScoped<INoticeService, NoticeService>();

builder.Services.AddScoped<ISocietySettingsService, SocietySettingsService>();

builder.Services.AddScoped<IDashboardService, DashboardService>();

builder.Services.AddScoped<IDocumentService, DocumentService>();


// =======================
// Email Settings
// =======================
builder.Services.Configure<EmailSettings>(
    builder.Configuration
    .GetSection("EmailSettings"));


// =======================
// Google Drive OAuth
// =======================
builder.Services.Configure<GoogleDriveSettings>(
    builder.Configuration
    .GetSection("GoogleDrive"));


builder.Services.AddSingleton<GoogleTokenService>();

builder.Services.AddScoped<IGoogleDriveService, GoogleDriveService>();


// =======================
// Swagger
// =======================
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer",
        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            Name = "Authorization",

            Type =
            Microsoft.OpenApi.Models.SecuritySchemeType.Http,

            Scheme = "bearer",

            BearerFormat = "JWT",

            In =
            Microsoft.OpenApi.Models.ParameterLocation.Header,

            Description =
            "Enter JWT Token"
        });


    options.AddSecurityRequirement(
        new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
        {
            {
                new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Reference =
                    new Microsoft.OpenApi.Models.OpenApiReference
                    {
                        Type =
                        Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,

                        Id = "Bearer"
                    }
                },

                Array.Empty<string>()
            }
        });
});



var app = builder.Build();


// =======================
// Middleware
// =======================

app.UseMiddleware<ExceptionMiddleware>();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}


app.UseHttpsRedirection();


app.UseCors("AllowAngular");


app.UseAuthentication();


app.UseAuthorization();


app.MapControllers();


app.Run();