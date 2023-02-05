using DeliveryApi.Data;
using DeliveryApi.Data.Context;
using DeliveryApi.Data.Context.Interfaces;
using DeliveryApi.Data.Repositories;
using DeliveryApi.Domain.Repositories.Interfaces;
using DeliveryApi.Domain.Services.Interfaces;
using DeliveryApi.Domain.Services;
using AutoMapper;
using DeliveryApi.Domain.Mapper;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
var value = Environment.GetEnvironmentVariable("CON_STRING");

System.Console.WriteLine("Connection string:");
System.Console.WriteLine(value);

// Add services to the container.
builder.Services.AddMemoryCache();

var config = new MapperConfiguration(cfg =>
    {
        cfg.AddProfile<ProdutoProfile>();
        cfg.AddProfile<CategoriaProfile>();
    });

IMapper mapper = config.CreateMapper();
builder.Services.Configure<DeliveryDatabaseSettings>(builder.Configuration.GetSection("DeliveryDatabase"))
                .AddSingleton(mapper)
                .AddScoped<IDatabaseContext, DatabaseContext>()
                .AddScoped<IProdutoRepository, ProdutoRepository>()
                .AddScoped<ICategoriaRepository, CategoriaRepository>()
                .AddScoped<ICategoriaService, CategoriaService>()
                .AddScoped<IProdutoService, ProdutoService>();

builder.Services.AddControllers()
                    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer()
                .AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI();
// }

if (!builder.Environment.IsDevelopment())
{
    builder.Services.AddHttpsRedirection(options =>
    {
        options.RedirectStatusCode = (int)HttpStatusCode.PermanentRedirect;
        options.HttpsPort = 443;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
