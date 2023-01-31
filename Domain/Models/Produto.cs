using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DeliveryApi.Domain.Models
{
    public class Adicional
    {
        [BsonElement("adicional")]
        public string? Nome { get; set; }
        [BsonElement("valorAdicional")]
        public Double Valor { get; set; }
    }
    public class Produto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("nome")]
        public string? Nome { get; set; }
        [BsonElement("img")]
        public string? UrlImagem { get; set; }
        [BsonElement("descricao")]
        public string? Descricao { get; set; }
        [BsonElement("categoria")]
        public string? Categoria { get; set; }
        [BsonElement("valor")]
        public Double Valor { get; set; }
        [BsonElement("adicionais")]
        public List<Adicional> Adicionais { get; set; }

        public Produto()
        {
            this.Adicionais = new List<Adicional>();
        }

    }
}