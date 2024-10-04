using AutoMapper;
using BaristaXpertControl.Application.Common.Persistences;
using BaristaXpertControl.Application.Common.Profiles;
using BaristaXpertControl.Application.Features.AuthManagement.RoleManagement.Commands;
using BaristaXpertControl.Infrastructure.Repositories;
using BaristaXpertControl.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using BaristaXpertControl.Application.Persistences;
using BaristaXpertControl.Infrastructure.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);


// Đăng ký AppDbContext với EntityFramework và cấu hình chuỗi kết nối
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Đăng ký MediatR với assembly chứa các Handler
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.AddMediatR(typeof(CreateRoleCommand).Assembly);

// Đăng ký UnitOfWork và các Repository
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
