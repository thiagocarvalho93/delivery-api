using DeliveryApi.Domain.DTOs.Produto;
using DeliveryApi.Domain.Exceptions;
using DeliveryApi.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApi.Api.Controllers
{
    [ApiController]
    [Route("api/produtos")]
    public class ProdutoController : Controller
    {
        private readonly IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        public async Task<IEnumerable<ProdutoResponseDTO>> ObterTodos() => await _produtoService.ObterTodosAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<ProdutoResponseDTO>> ObterPorId(string id)
        {
            try
            {
                return await _produtoService.ObterPorIdAsync(id);
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
        public async Task<IActionResult> Adicionar(ProdutoRequestDTO novoProduto)
        {
            try
            {
                await _produtoService.AdicionarAsync(novoProduto);

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
        public async Task<IActionResult> Atualizar(string id, ProdutoRequestDTO produtoAtualizado)
        {
            try
            {
                await _produtoService.AtualizarAsync(id, produtoAtualizado);

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
            var produto = await _produtoService.ObterPorIdAsync(id);

            if (produto is null)
            {
                return NotFound();
            }

            await _produtoService.RemoverAsync(id);

            return NoContent();
        }
    }
}