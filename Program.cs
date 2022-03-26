using BlogRest.Contexts;
using BlogRest.Dtos;
using BlogRest.Models;
using BlogRest.Repositories;
using BlogRest.Services;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Article Services

builder.Services.AddSingleton<ArticleConverter>();
builder.Services.AddSingleton<IArticleContext, ArticleContext>();
builder.Services.AddSingleton<IArticleRepository, ArticleRepository>();
builder.Services.AddSingleton<IArticleService, ArticleService>();

// Setting Services

builder.Services.AddSingleton<ISettingsService, SettingsService>();
builder.Services.AddSingleton<ISettingsRepository, SettingsRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "All",
        builder =>
        {
            builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseCors("All");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
    