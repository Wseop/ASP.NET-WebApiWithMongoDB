using LoaManagerApi.Models;
using LoaManagerApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// DI
builder.Services.Configure<AdminDatabaseSettings>(
    builder.Configuration.GetSection("AdminDatabase"));
builder.Services.AddSingleton<AdminService>();

// swagger
builder.Services.AddSwaggerGen();

var app = builder.Build();

// swagger
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
