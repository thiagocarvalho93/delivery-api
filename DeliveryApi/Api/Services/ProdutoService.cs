using DeliveryApi.Api.Exceptions;
using DeliveryApi.Domain.Models;
using DeliveryApi.Domain.Interfaces.Repositories;
using DeliveryApi.Domain.Interfaces.Services;
using Microsoft.Extensions.Caching.Memory;

namespace DeliveryApi.Api.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICategoriaService _categoriaService;
        public const string CACHE_KEY = "pd";
        // private readonly MemoryCacheEntryOptions cacheEntryOptions;
        private readonly IMemoryCache _memoryCache;

        public ProdutoService(IProdutoRepository produtoRepository, ICategoriaService categoriaService, IMemoryCache memoryCache)
        {
            _produtoRepository = produtoRepository;
            _categoriaService = categoriaService;
            _memoryCache = memoryCache;
        }

        public async Task<IEnumerable<Produto>> ObterTodosAsync()
        {
            var produtos = await _memoryCache.GetOrCreateAsync<IEnumerable<Produto>>(CACHE_KEY, async item =>
            {
                return await _produtoRepository.ObterTodosAsync();
            });

            return produtos;
        }

        public async Task<Produto> ObterPorIdAsync(string id)
        {
            var produto = await _memoryCache.GetOrCreateAsync<Produto>(CACHE_KEY + id, async item =>
            {
                return await _produtoRepository.ObterPorIdAsync(id);
            });
            if (produto is null)
            {
                throw new NotFoundException("Produto não encontrado");
            }

            return produto;
        }

        public async Task AdicionarAsync(Produto novoProduto)
        {
            if (novoProduto.Categoria is null)
            {
                throw new Exception("categoria é obrigatório");
            }
            await _categoriaService.ObterPorNomeAsync(novoProduto.Categoria);

            await _produtoRepository.AdicionarAsync(novoProduto);
            _memoryCache.Remove(CACHE_KEY);
        }

        public async Task AtualizarAsync(string id, Produto produtoAtualizado)
        {
            await ObterPorIdAsync(id);
            if (produtoAtualizado.Categoria is null)
            {
                throw new Exception("categoria é obrigatório");
            }

            produtoAtualizado.Id = id;
            await _categoriaService.ObterPorNomeAsync(produtoAtualizado.Categoria);

            await _produtoRepository.AtualizarAsync(id, produtoAtualizado);
            _memoryCache.Remove(CACHE_KEY);
            _memoryCache.Remove(CACHE_KEY + id);
        }

        public async Task RemoverAsync(string id)
        {
            await ObterPorIdAsync(id);

            await _produtoRepository.RemoverAsync(id);
            _memoryCache.Remove(CACHE_KEY);
            _memoryCache.Remove(CACHE_KEY + id);
        }
    }
}