using DeliveryApi.Domain.DTOs.Categoria;
using DeliveryApi.Domain.Exceptions;
using DeliveryApi.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApi.Api.Controllers
{
    [ApiController]
    [Route("api/categorias")]
    public class CategoriaController : Controller
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public async Task<IEnumerable<CategoriaResponseDTO>> ObterTodos() => await _categoriaService.ObterTodosAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<CategoriaResponseDTO>> ObterPorId(string id)
        {
            try
            {
                return await _categoriaService.ObterPorIdAsync(id);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("nome/{nome}")]
        public async Task<ActionResult<CategoriaResponseDTO>> ObterPorNome(string nome)
        {
            try
            {
                return await _categoriaService.ObterPorNomeAsync(nome);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar(CategoriaRequestDTO request)
        {
            try
            {
                await _categoriaService.AdicionarAsync(request);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Atualizar(string id, CategoriaRequestDTO request)
        {
            try
            {
                await _categoriaService.AtualizarAsync(id, request);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Remover(string id)
        {
            try
            {
                await _categoriaService.RemoverAsync(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}