using Microsoft.EntityFrameworkCore;
using Domain;
using Repositories;
using Repositories.Interfaces;
using Tasks_Backend.Services.Interfaces;
using Tasks_Backend.Services;
using Tasks_Backend.Models;
using Tasks_Backend.Middlewares;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(connectionString,
        ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
builder.Services.AddScoped<ISecurityService, SecurityService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
var clientWebsite = builder.Configuration.GetSection("AppSettings")["FrontendUrl"];
var aplicationWebsite = builder.Configuration.GetSection("AppSettings")["WebsiteUrl"];
builder.Services.AddCors(options =>
{
    options.AddPolicy(name:"MyPolicy",
        policy =>
        {
            policy.WithOrigins(clientWebsite, aplicationWebsite)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors("MyPolicy");

app.UseHttpsRedirection();

app.UseMiddleware<JwtMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();