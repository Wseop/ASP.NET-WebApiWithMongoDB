using WebApiMongoDB.Models;
using WebApiMongoDB.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// 종속성 주입 (Dependency Injection)
builder.Services.Configure<ProfileDatabaseSettings>(
    builder.Configuration.GetSection("ProfileDatabase"));

builder.Services.AddSingleton<ProfileService>();

// Swagger 미들웨어 추가
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger 미들웨어 추가
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
