using AutoMapper;
using DeliveryApi.Domain.DTOs.Categoria;
using DeliveryApi.Domain.Exceptions;
using DeliveryApi.Domain.Models;
using DeliveryApi.Domain.Repositories.Interfaces;
using DeliveryApi.Domain.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace DeliveryApi.Domain.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        public const string CACHE_KEY = "ct";
        private readonly MemoryCacheEntryOptions cacheEntryOptions;
        private readonly IMemoryCache _memoryCache;
        private readonly IMapper _mapper;

        public CategoriaService(ICategoriaRepository categoriaRepository, IMemoryCache memoryCache, IMapper mapper)
        {
            _categoriaRepository = categoriaRepository;
            _memoryCache = memoryCache;
            _mapper = mapper;
            cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromDays(1));
        }

        public async Task<IEnumerable<CategoriaResponseDTO>> ObterTodosAsync()
        {
            var categorias = await _memoryCache.GetOrCreateAsync<IEnumerable<Categoria>>(CACHE_KEY, async item =>
            {
                return await _categoriaRepository.ObterTodosAsync();
            });

            return _mapper.Map<IEnumerable<Categoria>, IEnumerable<CategoriaResponseDTO>>(categorias);
        }

        public async Task<CategoriaResponseDTO> ObterPorIdAsync(string id)
        {
            var categoria = await _memoryCache.GetOrCreateAsync<Categoria>(CACHE_KEY + id, async item =>
            {
                return await _categoriaRepository.ObterPorIdAsync(id);
            });

            if (categoria is null)
            {
                throw new NotFoundException("Categoria não encontrada");
            }
            return _mapper.Map<Categoria, CategoriaResponseDTO>(categoria);
        }

        public async Task<CategoriaResponseDTO> ObterPorNomeAsync(string nomeCategoria)
        {
            var categoria = await _memoryCache.GetOrCreateAsync<Categoria>(CACHE_KEY + nomeCategoria, async item =>
            {
                return await _categoriaRepository.ObterPorNomeAsync(nomeCategoria);
            });

            if (categoria is null)
            {
                throw new NotFoundException("Categoria não encontrada");
            }
            return _mapper.Map<Categoria, CategoriaResponseDTO>(categoria);
        }

        public async Task AdicionarAsync(CategoriaRequestDTO novoCategoria)
        {
            await _categoriaRepository.AdicionarAsync(_mapper.Map<CategoriaRequestDTO, Categoria>(novoCategoria));
            _memoryCache.Remove(CACHE_KEY);
        }

        public async Task AtualizarAsync(string id, CategoriaRequestDTO request)
        {
            var categoriaAntes = await ObterPorIdAsync(id);
            var categoriaAtualizada = _mapper.Map<CategoriaRequestDTO, Categoria>(request);

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