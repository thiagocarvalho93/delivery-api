using DeliveryApi.Domain.Models;

namespace DeliveryApi.Domain.Interfaces.Services
{
    public interface ICategoriaService
    {
        public Task<IEnumerable<Categoria>> ObterTodosAsync();
        public Task<Categoria> ObterPorIdAsync(string id);
        public Task<Categoria> ObterPorNomeAsync(string nome);
        public Task AdicionarAsync(Categoria newCategoria);
        public Task AtualizarAsync(string id, Categoria updatedCategoria);
        public Task RemoverAsync(string id);
    }
}