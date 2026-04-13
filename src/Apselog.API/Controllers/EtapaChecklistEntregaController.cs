using Apselog.Application.DTOs.Request.EtapaChecklistEntrega;
using Apselog.Application.UseCases.Interfaces.EtapaChecklistEntrega;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Apselog.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class EtapaChecklistEntregaController : ControllerBase
{
    private readonly ICriarEtapaChecklistEntregaUseCase _criarEtapaChecklistEntregaUseCase;
    private readonly IAtualizarEtapaChecklistEntregaUseCase _atualizarEtapaChecklistEntregaUseCase;
    private readonly IListarEtapaChecklistEntregaUseCase _listarEtapaChecklistEntregaUseCase;
    private readonly IExcluirEtapaChecklistEntregaUseCase _excluirEtapaChecklistEntregaUseCase;

    public EtapaChecklistEntregaController(
        ICriarEtapaChecklistEntregaUseCase criarEtapaChecklistEntregaUseCase,
        IAtualizarEtapaChecklistEntregaUseCase atualizarEtapaChecklistEntregaUseCase,
        IListarEtapaChecklistEntregaUseCase listarEtapaChecklistEntregaUseCase,
        IExcluirEtapaChecklistEntregaUseCase excluirEtapaChecklistEntregaUseCase)
    {
        _criarEtapaChecklistEntregaUseCase = criarEtapaChecklistEntregaUseCase;
        _atualizarEtapaChecklistEntregaUseCase = atualizarEtapaChecklistEntregaUseCase;
        _listarEtapaChecklistEntregaUseCase = listarEtapaChecklistEntregaUseCase;
        _excluirEtapaChecklistEntregaUseCase = excluirEtapaChecklistEntregaUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> CriarAsync([FromBody] CriarEtapaChecklistEntregaRequest request)
    {
        try
        {
            var response = await _criarEtapaChecklistEntregaUseCase.ExecutarAsync(request);
            return CreatedAtAction(nameof(CriarAsync), new { id = response.Id }, response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> ListarAsync([FromQuery] ListarEtapaChecklistEntregaRequest request)
    {
        try
        {
            var response = await _listarEtapaChecklistEntregaUseCase.ExecutarAsync(request);
            return Ok(response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> AtualizarAsync(Guid id, [FromBody] AtualizarEtapaChecklistEntregaRequest request)
    {
        try
        {
            request.Id = id;

            var response = await _atualizarEtapaChecklistEntregaUseCase.ExecutarAsync(request);
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
            var response = await _excluirEtapaChecklistEntregaUseCase.ExecutarAsync(new ExcluirEtapaChecklistEntregaRequest { Id = id });
            return Ok(response);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
    }
}
