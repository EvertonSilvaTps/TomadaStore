using TomadasSore.CustomerAPI.Data;
using TomadasSore.CustomerAPI.Repository;
using TomadasSore.CustomerAPI.Repository.Interfaces;
using TomadasSore.CustomerAPI.Services;
using TomadasSore.CustomerAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton<ConnectionDB>();

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();




var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
