using Apselog.Application.DTOs.Request;
using Apselog.Application.UseCases.Interfaces.Entrega;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Apselog.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class EntregaController : ControllerBase
{
    private readonly ICriarEntregaUseCase _criarEntregaUseCase;
    private readonly IAtualizarEntregaUseCase _atualizarEntregaUseCase;
    private readonly IListarEntregaUseCase _listarEntregaUseCase;
    private readonly IExcluirEntregaUseCase _excluirEntregaUseCase;

    public EntregaController(
        ICriarEntregaUseCase criarEntregaUseCase,
        IAtualizarEntregaUseCase atualizarEntregaUseCase,
        IListarEntregaUseCase listarEntregaUseCase,
        IExcluirEntregaUseCase excluirEntregaUseCase)
    {
        _criarEntregaUseCase = criarEntregaUseCase;
        _atualizarEntregaUseCase = atualizarEntregaUseCase;
        _listarEntregaUseCase = listarEntregaUseCase;
        _excluirEntregaUseCase = excluirEntregaUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> CriarAsync([FromBody] CriarEntregaRequest request)
    {
        try
        {
            var response = await _criarEntregaUseCase.ExecutarAsync(request);
            return CreatedAtAction(nameof(CriarAsync), new { id = response.Id }, response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> ListarAsync([FromQuery] ListarEntregaRequest request)
    {
        try
        {
            var response = await _listarEntregaUseCase.ExecutarAsync(request);
            return Ok(response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> AtualizarAsync(Guid id, [FromBody] AtualizarEntregaRequest request)
    {
        try
        {
            request.Id = id;

            var response = await _atualizarEntregaUseCase.ExecutarAsync(request);
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
            var response = await _excluirEntregaUseCase.ExecutarAsync(new ExcluirEntregaRequest { Id = id });
            return Ok(response);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
    }
}
