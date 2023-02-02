using DeliveryApi.Domain.Models;
using MongoDB.Driver;

namespace DeliveryApi.Data.Context.Interfaces
{
    public interface IDatabaseContext
    {
        public IMongoCollection<Produto> Produtos { get; }
        public IMongoCollection<Categoria> Categorias { get; }
    }
}