using AutoMapper;
using DeliveryApi.Domain.DTOs.Produto;
using DeliveryApi.Domain.Exceptions;
using DeliveryApi.Domain.Models;
using DeliveryApi.Domain.Repositories.Interfaces;
using DeliveryApi.Domain.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace DeliveryApi.Domain.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICategoriaService _categoriaService;
        public const string CACHE_KEY = "pd";
        // private readonly MemoryCacheEntryOptions cacheEntryOptions;
        private readonly IMemoryCache _memoryCache;
        private readonly IMapper _mapper;

        public ProdutoService(IProdutoRepository produtoRepository, ICategoriaService categoriaService, IMemoryCache memoryCache, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _categoriaService = categoriaService;
            _memoryCache = memoryCache;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProdutoResponseDTO>> ObterTodosAsync()
        {
            var produtos = await _memoryCache.GetOrCreateAsync<IEnumerable<Produto>>(CACHE_KEY, async item =>
            {
                return await _produtoRepository.ObterTodosAsync();
            });

            return _mapper.Map<IEnumerable<Produto>, IEnumerable<ProdutoResponseDTO>>(produtos);
        }

        public async Task<ProdutoResponseDTO> ObterPorIdAsync(string id)
        {
            var produto = await _memoryCache.GetOrCreateAsync<Produto>(CACHE_KEY + id, async item =>
            {
                return await _produtoRepository.ObterPorIdAsync(id);
            });
            if (produto is null)
            {
                throw new NotFoundException("Produto não encontrado");
            }

            return _mapper.Map<Produto, ProdutoResponseDTO>(produto);
        }

        public async Task AdicionarAsync(ProdutoRequestDTO novoProduto)
        {
            if (novoProduto.Categoria is null)
            {
                throw new Exception("categoria é obrigatório");
            }
            await _categoriaService.ObterPorNomeAsync(novoProduto.Categoria);

            await _produtoRepository.AdicionarAsync(_mapper.Map<ProdutoRequestDTO, Produto>(novoProduto));
            _memoryCache.Remove(CACHE_KEY);
        }

        public async Task AtualizarAsync(string id, ProdutoRequestDTO request)
        {
            await ObterPorIdAsync(id);
            if (request.Categoria is null)
            {
                throw new Exception("categoria é obrigatório");
            }
            var produtoAtualizado = _mapper.Map<ProdutoRequestDTO, Produto>(request);

            produtoAtualizado.Id = id;
            await _categoriaService.ObterPorNomeAsync(request.Categoria);

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