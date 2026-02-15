using exe1.data;
using exe1.Interfaces;
using exe1.Repositories;
using exe1.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// --- 1. הגדרות קונטרולרים ---
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddEndpointsApiExplorer();

// --- 2. הגדרות Swagger ---
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "נא להזין טוקן ללא המילה Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            new string[] {}
        }
    });
});

// --- 3. הגדרות אימות (Authentication) - מעודכן לסנכרון עם TokenService ---
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"] ?? "YourDefaultFallbackKeyIfMissing123!";

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"], // יקרא "exe1" מהקובץ
            ValidAudience = jwtSettings["Audience"], // יקרא "AngularApp" מהקובץ
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        };
    });

// --- 4. מסד נתונים והזרקות ---
builder.Services.AddDbContext<ApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("connection")));

builder.Services.AddScoped<IPrizeRepozitory, Prize_repository>();
builder.Services.AddScoped<IPrizeService, Prize_service>();
builder.Services.AddScoped<IDonorService, Donor_service>();
builder.Services.AddScoped<IDonorRepository, Doner_repository>();
builder.Services.AddScoped<IBasketRepository, Basket_repository>();
builder.Services.AddScoped<IBasketService, Basket_service>();
builder.Services.AddScoped<IUserRepository, User_repository>();
builder.Services.AddScoped<IUserService, User_service>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IRandomService, Random_service>();
builder.Services.AddScoped<IRandomRepository, Random_repository>();

// --- 5. הגדרות CORS ---
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// --- 6. Middleware Pipeline ---
app.UseCors("AllowAngular");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // חייב לבוא לפני Authorization
app.UseAuthorization();

app.UseStaticFiles();
app.MapControllers();

app.Run();