using DeliveryApi.Domain.DTOs.Categoria;
using DeliveryApi.Domain.Models;

namespace DeliveryApi.Domain.Services.Interfaces
{
    public interface ICategoriaService
    {
        public Task<IEnumerable<CategoriaResponseDTO>> ObterTodosAsync();
        public Task<CategoriaResponseDTO> ObterPorIdAsync(string id);
        public Task<CategoriaResponseDTO> ObterPorNomeAsync(string nome);
        public Task AdicionarAsync(CategoriaRequestDTO newCategoria);
        public Task AtualizarAsync(string id, CategoriaRequestDTO updatedCategoria);
        public Task RemoverAsync(string id);
    }
}