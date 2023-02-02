using DeliveryApi.Data.Context.Interfaces;
using DeliveryApi.Domain.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DeliveryApi.Data.Context
{
    public class DatabaseContext : IDatabaseContext
    {
        public IMongoCollection<Produto> Produtos { get; }
        public IMongoCollection<Categoria> Categorias { get; }

        public DatabaseContext(IOptions<DeliveryDatabaseSettings> deliveryDatabaseSettings)
        {
            var mongoClient = new MongoClient(Environment.GetEnvironmentVariable("CON_STRING"));
            var mongoDatabase = mongoClient.GetDatabase(deliveryDatabaseSettings.Value.DatabaseName);
            Produtos = mongoDatabase.GetCollection<Produto>(deliveryDatabaseSettings.Value.ProdutosCollectionName);
            Categorias = mongoDatabase.GetCollection<Categoria>(deliveryDatabaseSettings.Value.CategoriasCollectionName);
        }
    }
}