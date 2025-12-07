using TomadaStore.SaleAPI.Data;
using TomadaStore.SaleAPI.Repositories;
using TomadaStore.SaleAPI.Repositories.Interfaces;
using TomadaStore.SaleAPI.Services;
using TomadaStore.SaleAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDB"));

builder.Services.AddScoped<ConnectionDB>();

builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<ISaleService, SaleService>();


// vai acessar a API da Uri informada, buscando o comando IHtppClientFactory com a menção "Customer"
builder.Services.AddHttpClient("Customer", client => client.BaseAddress = new Uri("https://localhost:5001/api/v1/Customer"));

// vai acessar a API da Uri informada, buscando o comando IHtppClientFactory com a menção "Product"
builder.Services.AddHttpClient("Product", client => client.BaseAddress = new Uri("https://localhost:6001/api/v1/Product"));



var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
