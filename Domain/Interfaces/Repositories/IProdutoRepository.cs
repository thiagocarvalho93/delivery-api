using DeliveryApi.Domain.Models;

namespace DeliveryApi.Domain.Interfaces.Repositories
{
    public interface IProdutoRepository
    {
        public Task<List<Produto>> ObterTodosAsync();
        public Task<Produto?> ObterPorIdAsync(string id);
        public Task AdicionarAsync(Produto newProduto);
        public Task AtualizarAsync(string id, Produto updatedProduto);
        public Task RemoverAsync(string id);
    }
}