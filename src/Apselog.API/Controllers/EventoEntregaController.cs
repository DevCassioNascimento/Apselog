using Apselog.Application.DTOs.Request.EventoEntrega;
using Apselog.Application.UseCases.Interfaces.EventoEntrega;
using Microsoft.AspNetCore.Mvc;

namespace Apselog.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventoEntregaController : ControllerBase
{
    private readonly ICriarEventoEntregaUseCase _criarEventoEntregaUseCase;
    private readonly IAtualizarEventoEntregaUseCase _atualizarEventoEntregaUseCase;
    private readonly IListarEventoEntregaUseCase _listarEventoEntregaUseCase;
    private readonly IExcluirEventoEntregaUseCase _excluirEventoEntregaUseCase;

    public EventoEntregaController(
        ICriarEventoEntregaUseCase criarEventoEntregaUseCase,
        IAtualizarEventoEntregaUseCase atualizarEventoEntregaUseCase,
        IListarEventoEntregaUseCase listarEventoEntregaUseCase,
        IExcluirEventoEntregaUseCase excluirEventoEntregaUseCase)
    {
        _criarEventoEntregaUseCase = criarEventoEntregaUseCase;
        _atualizarEventoEntregaUseCase = atualizarEventoEntregaUseCase;
        _listarEventoEntregaUseCase = listarEventoEntregaUseCase;
        _excluirEventoEntregaUseCase = excluirEventoEntregaUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> CriarAsync([FromBody] CriarEventoEntregaRequest request)
    {
        try
        {
            var response = await _criarEventoEntregaUseCase.ExecutarAsync(request);
            return CreatedAtAction(nameof(CriarAsync), new { id = response.Id }, response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> ListarAsync([FromQuery] ListarEventoEntregaRequest request)
    {
        try
        {
            var response = await _listarEventoEntregaUseCase.ExecutarAsync(request);
            return Ok(response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> AtualizarAsync(Guid id, [FromBody] AtualizarEventoEntregaRequest request)
    {
        try
        {
            request.Id = id;

            var response = await _atualizarEventoEntregaUseCase.ExecutarAsync(request);
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
            var response = await _excluirEventoEntregaUseCase.ExecutarAsync(new ExcluirEventoEntregaRequest { Id = id });
            return Ok(response);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
    }
}
