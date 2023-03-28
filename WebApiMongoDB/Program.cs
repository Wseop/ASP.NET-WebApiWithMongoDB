using WebApiMongoDB.Models;
using WebApiMongoDB.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// ���Ӽ� ���� (Dependency Injection)
builder.Services.Configure<ProfileDatabaseSettings>(
    builder.Configuration.GetSection("ProfileDatabase"));

builder.Services.AddSingleton<ProfileService>();

// Swagger �̵���� �߰�
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger �̵���� �߰�
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
