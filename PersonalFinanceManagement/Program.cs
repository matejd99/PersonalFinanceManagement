using Microsoft.EntityFrameworkCore;
using PersonalFinanceManagement.Data;
using PersonalFinanceManagement.Data.Repository;
using PersonalFinanceManagement.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connection = @"data source=127.0.0.1;User Id=sa;Password=Secret1234";
builder.Services.AddDbContext<PFMDbContext>(
        options => options.UseSqlServer(connection));

builder.Services.AddScoped<TransactionsService>();
builder.Services.AddScoped<CategoriesService>();
builder.Services.AddScoped<TransactionRepository>();
builder.Services.AddScoped<CategoryRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder
     .AllowAnyOrigin()
     .AllowAnyMethod()
     .AllowAnyHeader());

app.UseAuthorization();

app.MapControllers();

app.Run();