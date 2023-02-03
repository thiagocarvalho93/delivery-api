using MongoDB.Bson.Serialization.Attributes;

namespace DeliveryApi.Domain.DTOs.Produto
{
    public class AdicionalDTO
    {
        [BsonElement("adicional")]
        public string? Nome { get; set; }
        [BsonElement("valorAdicional")]
        public Double Valor { get; set; }
    }
}