using DeliveryApi.Data;
using DeliveryApi.Data.Context;
using DeliveryApi.Data.Context.Interfaces;
using DeliveryApi.Data.Repositories;
using DeliveryApi.Domain.Interfaces.Repositories;
using DeliveryApi.Domain.Interfaces.Services;
using DeliveryApi.Api.Services;

var builder = WebApplication.CreateBuilder(args);
var value = Environment.GetEnvironmentVariable("CON_STRING");

System.Console.WriteLine("Connection string:");
System.Console.WriteLine(value);

// Add services to the container.
builder.Services.AddMemoryCache();
builder.Services.Configure<DeliveryDatabaseSettings>(builder.Configuration.GetSection("DeliveryDatabase"));
builder.Services.AddScoped<IDatabaseContext, DatabaseContext>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();

builder.Services.AddControllers()
                    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
