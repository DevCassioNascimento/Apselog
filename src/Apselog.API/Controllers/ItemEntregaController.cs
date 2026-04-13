using Apselog.Application.DTOs.Request.ItemEntrega;
using Apselog.Application.UseCases.Interfaces.ItemEntrega;
using Microsoft.AspNetCore.Mvc;

namespace Apselog.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemEntregaController : ControllerBase
{
    private readonly ICriarItemEntregaUseCase _criarItemEntregaUseCase;
    private readonly IAtualizarItemEntregaUseCase _atualizarItemEntregaUseCase;
    private readonly IListarItemEntregaUseCase _listarItemEntregaUseCase;
    private readonly IExcluirItemEntregaUseCase _excluirItemEntregaUseCase;

    public ItemEntregaController(
        ICriarItemEntregaUseCase criarItemEntregaUseCase,
        IAtualizarItemEntregaUseCase atualizarItemEntregaUseCase,
        IListarItemEntregaUseCase listarItemEntregaUseCase,
        IExcluirItemEntregaUseCase excluirItemEntregaUseCase)
    {
        _criarItemEntregaUseCase = criarItemEntregaUseCase;
        _atualizarItemEntregaUseCase = atualizarItemEntregaUseCase;
        _listarItemEntregaUseCase = listarItemEntregaUseCase;
        _excluirItemEntregaUseCase = excluirItemEntregaUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> CriarAsync([FromBody] CriarItemEntregaRequest request)
    {
        try
        {
            var response = await _criarItemEntregaUseCase.ExecutarAsync(request);
            return CreatedAtAction(nameof(CriarAsync), new { id = response.Id }, response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> ListarAsync([FromQuery] ListarItemEntregaRequest request)
    {
        try
        {
            var response = await _listarItemEntregaUseCase.ExecutarAsync(request);
            return Ok(response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> AtualizarAsync(Guid id, [FromBody] AtualizarItemEntregaRequest request)
    {
        try
        {
            request.Id = id;

            var response = await _atualizarItemEntregaUseCase.ExecutarAsync(request);
            return Ok(response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> ExcluirAsync(Guid id)
    {
        try
        {
            var response = await _excluirItemEntregaUseCase.ExecutarAsync(new ExcluirItemEntregaRequest { Id = id });
            return Ok(response);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
    }
}
