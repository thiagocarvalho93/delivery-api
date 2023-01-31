using MongoDB.Driver;
using DeliveryApi.Data.Context.Interfaces;
using DeliveryApi.Domain.Models;
using DeliveryApi.Domain.Interfaces.Repositories;

namespace DeliveryApi.Data.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly IDatabaseContext _context;

        public ProdutoRepository(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Produto>> ObterTodosAsync() => await _context.Produtos.Find(_ => true).ToListAsync();

        public async Task<Produto?> ObterPorIdAsync(string id) => await _context.Produtos.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task AdicionarAsync(Produto newProduto) => await _context.Produtos.InsertOneAsync(newProduto);

        public async Task AtualizarAsync(string id, Produto updatedProduto) => await _context.Produtos.ReplaceOneAsync(x => x.Id == id, updatedProduto);

        public async Task RemoverAsync(string id) => await _context.Produtos.DeleteOneAsync(x => x.Id == id);
    }
}