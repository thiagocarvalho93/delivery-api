using MongoDB.Bson.Serialization.Attributes;

namespace DeliveryApi.Domain.DTOs.Produto
{
    public class ProdutoRequestDTO
    {
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
        public IEnumerable<AdicionalDTO> Adicionais { get; set; }

        public ProdutoRequestDTO()
        {
            this.Adicionais = new List<AdicionalDTO>();
        }
    }
}