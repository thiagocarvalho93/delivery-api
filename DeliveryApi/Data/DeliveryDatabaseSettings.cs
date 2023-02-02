namespace DeliveryApi.Data
{
    public class DeliveryDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string ProdutosCollectionName { get; set; } = null!;
        public string CategoriasCollectionName { get; set; } = null!;
    }
}