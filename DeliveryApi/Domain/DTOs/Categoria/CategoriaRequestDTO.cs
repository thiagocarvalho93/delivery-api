using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DeliveryApi.Domain.DTOs.Categoria
{
    public class CategoriaRequestDTO
    {
        [BsonElement("nome")]
        public string? Nome { get; set; }
        [BsonElement("img")]
        public string? UrlImagem { get; set; }
    }
}