using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DeliveryApi.Domain.DTOs.Categoria
{
    public class CategoriaResponseDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("nome")]
        public string? Nome { get; set; }
        [BsonElement("img")]
        public string? UrlImagem { get; set; }
    }
}