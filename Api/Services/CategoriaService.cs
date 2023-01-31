using DeliveryApi.Api.Exceptions;
using DeliveryApi.Domain.Models;
using DeliveryApi.Domain.Interfaces.Repositories;
using DeliveryApi.Domain.Interfaces.Services;
using Microsoft.Extensions.Caching.Memory;

namespace DeliveryApi.Api.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        public const string CACHE_KEY = "ct";
        private readonly MemoryCacheEntryOptions cacheEntryOptions;
        private readonly IMemoryCache _memoryCache;

        public CategoriaService(ICategoriaRepository categoriaRepository, IMemoryCache memoryCache)
        {
            _categoriaRepository = categoriaRepository;
            _memoryCache = memoryCache;
            cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromDays(1));
        }

        public async Task<List<Categoria>> ObterTodosAsync()
        {
            var categorias = await _memoryCache.GetOrCreateAsync<List<Categoria>>(CACHE_KEY, async item =>
            {
                return await _categoriaRepository.ObterTodosAsync();
            });

            return categorias;
        }

        public async Task<Categoria> ObterPorIdAsync(string id)
        {
            var categoria = await _memoryCache.GetOrCreateAsync<Categoria>(CACHE_KEY + id, async item =>
            {
                return await _categoriaRepository.ObterPorIdAsync(id);
            });

            if (categoria is null)
            {
                throw new NotFoundException("Categoria não encontrada");
            }
            return categoria;
        }

        public async Task<Categoria> ObterPorNomeAsync(string nomeCategoria)
        {
            var categoria = await _memoryCache.GetOrCreateAsync<Categoria>(CACHE_KEY + nomeCategoria, async item =>
            {
                return await _categoriaRepository.ObterPorIdAsync(nomeCategoria);
            });

            if (categoria is null)
            {
                throw new NotFoundException("Categoria não encontrada");
            }
            return categoria;
        }

        public async Task AdicionarAsync(Categoria novoCategoria)
        {
            await _categoriaRepository.AdicionarAsync(novoCategoria);
            _memoryCache.Remove(CACHE_KEY);
        }

        public async Task AtualizarAsync(string id, Categoria categoriaAtualizada)
        {
            var categoriaAntes = await ObterPorIdAsync(id);

            categoriaAtualizada.Id = id;
            await _categoriaRepository.AtualizarAsync(id, categoriaAtualizada);

            _memoryCache.Remove(CACHE_KEY + id);
            _memoryCache.Remove(CACHE_KEY + categoriaAntes.Nome);
        }

        public async Task RemoverAsync(string id)
        {
            var categoriaApagada = await ObterPorIdAsync(id);
            await _categoriaRepository.RemoverAsync(id);

            _memoryCache.Remove(CACHE_KEY + id);
            _memoryCache.Remove(CACHE_KEY + categoriaApagada.Nome);
        }
    }
}