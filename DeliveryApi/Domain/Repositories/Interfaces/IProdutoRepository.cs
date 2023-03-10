using DeliveryApi.Domain.Models;

namespace DeliveryApi.Domain.Repositories.Interfaces
{
    public interface IProdutoRepository
    {
        public Task<IEnumerable<Produto>> ObterTodosAsync();
        public Task<Produto?> ObterPorIdAsync(string id);
        public Task AdicionarAsync(Produto newProduto);
        public Task AtualizarAsync(string id, Produto updatedProduto);
        public Task RemoverAsync(string id);
    }
}