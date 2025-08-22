using BankRiskTracking.Business.Services;
using BankRiskTracking.DataAccess;
using BankRiskTracking.DataAccess.Repository;
using BankRiskTracking.Entities.Entities;
using BankRiskTracking.Entities.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
// JWT & Swagger
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;

Log.Logger = new LoggerConfiguration()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

// ==== Rate Limiter (SENDEKİ GİBİ) ====
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("RateLimiter", opt =>
    {
        opt.PermitLimit = 5;
        opt.Window = TimeSpan.FromSeconds(10);
        opt.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
        opt.QueueLimit = 2;
    });
    options.AddFixedWindowLimiter("RateLimiter2", opt =>
    {
        opt.PermitLimit = 5;
        opt.Window = TimeSpan.FromSeconds(10);
    });
});

// ==== DbContext ====
builder.Services.AddDbContext<DatabaseConnection>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddScoped<IRiskHistoryService, RiskHistoryService>();    
 

// ==== DI (Repository + Servisler) ====
// Tek satır açık jenerik repo (TÜM T'ler için yeterli):
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// Servisler (her biri bir kez):
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerNoteService, CustomerNoteService>();
builder.Services.AddScoped<IRiskHistoryService, RiskHistoryService>(); // varsa aç
builder.Services.AddScoped<ITransaction, TransactionServices>(); // varsa aç

// ==== JWT Authentication + Authorization ====
// appsettings.json içine şunlar olmalı:
// "Jwt": { "Issuer": "BankRiskApi", "Audience": "BankRiskClient", "Key": "EN_AZ_32_KARAKTER_GIZLI_ANAHTAR" }
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddAuthorization();

// ==== Controllers + Swagger (Bearer düğmesi dahil) ====
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Sadece JWT token'ı gir (başına 'Bearer ' yazma).",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };

    c.AddSecurityDefinition("Bearer", securityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { securityScheme, Array.Empty<string>() }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Sende zaten vardı: açık bırakıyorum
app.UseRateLimiter();

// JWT pipeline (ZORUNLU SIRA)
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
