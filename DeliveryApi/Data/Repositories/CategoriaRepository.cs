using DeliveryApi.Data.Context.Interfaces;
using DeliveryApi.Domain.Models;
using DeliveryApi.Domain.Interfaces.Repositories;
using MongoDB.Driver;

namespace DeliveryApi.Data.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly IDatabaseContext _context;

        public CategoriaRepository(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Categoria>> ObterTodosAsync() => await _context.Categorias.Find(_ => true).ToListAsync();

        public async Task<Categoria?> ObterPorIdAsync(string id) => await _context.Categorias.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<Categoria?> ObterPorNomeAsync(string nome) => await _context.Categorias.Find(x => x.Nome == nome).FirstOrDefaultAsync();

        public async Task AdicionarAsync(Categoria newCategoria) => await _context.Categorias.InsertOneAsync(newCategoria);

        public async Task AtualizarAsync(string id, Categoria updatedCategoria) => await _context.Categorias.ReplaceOneAsync(x => x.Id == id, updatedCategoria);

        public async Task RemoverAsync(string id) => await _context.Categorias.DeleteOneAsync(x => x.Id == id);
    }
}