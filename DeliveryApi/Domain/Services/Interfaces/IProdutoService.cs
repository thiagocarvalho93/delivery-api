using DeliveryApi.Domain.DTOs.Produto;

namespace DeliveryApi.Domain.Services.Interfaces
{
    public interface IProdutoService
    {
        public Task<IEnumerable<ProdutoResponseDTO>> ObterTodosAsync();
        public Task<ProdutoResponseDTO> ObterPorIdAsync(string id);
        public Task AdicionarAsync(ProdutoRequestDTO request);
        public Task AtualizarAsync(string id, ProdutoRequestDTO request);
        public Task RemoverAsync(string id);
    }
}