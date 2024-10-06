using AutoMapper;
using BaristaXpertControl.Application.Common.Profiles;
using BaristaXpertControl.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using BaristaXpertControl.Application.Persistences;
using BaristaXpertControl.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using BaristaXpertControl.Application.Common.Persistences;
using BaristaXpertControl.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Đăng ký ApplicationDbContext với EntityFramework và cấu hình chuỗi kết nối
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Đăng ký ASP.NET Core Identity với IdentityUser và IdentityRole, sử dụng ApplicationDbContext
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Cấu hình JWT Authentication
var secretKey = builder.Configuration["Jwt:SecretKey"];
var key = Encoding.ASCII.GetBytes(secretKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false, 
        ValidateAudience = false, 
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

// Swagger configuration
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BaristaXpertControl API", Version = "v1" });

    // Cấu hình JWT cho Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your token in the text input below.\r\n\r\nExample: \"12345abcdef\""
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
            new string[] {}
        }
    });
});

// Đăng ký AutoMapper với assembly chứa các profile
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// Đăng ký MediatR với assembly chứa các Handler
builder.Services.AddMediatR(typeof(Program).Assembly);
builder.Services.AddMediatR(typeof(BaristaXpertControl.Application.Features.StoreManagement.Handlers.CreateStoreHandler).Assembly);

// Đăng ký UnitOfWork và các Repository
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IStoreRepository, StoreRepository>();

// Đăng ký các dịch vụ Controllers
builder.Services.AddControllers();

// Swagger configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BaristaXpertControl API v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseMiddleware<BaristaXpertControl.API.Middlewares.CustomJwtMiddleware>();

app.UseHttpsRedirection();

// Middleware Authentication và Authorization
app.UseAuthentication();  
app.UseAuthorization();  

// Map Controllers
app.MapControllers();

app.Run();
