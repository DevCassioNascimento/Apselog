using Apselog.Application.DTOs.Request.EtapaChecklistModelo;
using Apselog.Application.UseCases.Interfaces.EtapaChecklistModelo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Apselog.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class EtapaChecklistModeloController : ControllerBase
{
    private readonly ICriarEtapaChecklistModeloUseCase _criarEtapaChecklistModeloUseCase;
    private readonly IAtualizarEtapaChecklistModeloUseCase _atualizarEtapaChecklistModeloUseCase;
    private readonly IListarEtapaChecklistModeloUseCase _listarEtapaChecklistModeloUseCase;
    private readonly IExcluirEtapaChecklistModeloUseCase _excluirEtapaChecklistModeloUseCase;

    public EtapaChecklistModeloController(
        ICriarEtapaChecklistModeloUseCase criarEtapaChecklistModeloUseCase,
        IAtualizarEtapaChecklistModeloUseCase atualizarEtapaChecklistModeloUseCase,
        IListarEtapaChecklistModeloUseCase listarEtapaChecklistModeloUseCase,
        IExcluirEtapaChecklistModeloUseCase excluirEtapaChecklistModeloUseCase)
    {
        _criarEtapaChecklistModeloUseCase = criarEtapaChecklistModeloUseCase;
        _atualizarEtapaChecklistModeloUseCase = atualizarEtapaChecklistModeloUseCase;
        _listarEtapaChecklistModeloUseCase = listarEtapaChecklistModeloUseCase;
        _excluirEtapaChecklistModeloUseCase = excluirEtapaChecklistModeloUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> CriarAsync([FromBody] CriarEtapaChecklistModeloRequest request)
    {
        try
        {
            var response = await _criarEtapaChecklistModeloUseCase.ExecutarAsync(request);
            return CreatedAtAction(nameof(CriarAsync), new { id = response.Id }, response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> ListarAsync([FromQuery] ListarEtapaChecklistModeloRequest request)
    {
        try
        {
            var response = await _listarEtapaChecklistModeloUseCase.ExecutarAsync(request);
            return Ok(response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> AtualizarAsync(Guid id, [FromBody] AtualizarEtapaChecklistModeloRequest request)
    {
        try
        {
            request.Id = id;

            var response = await _atualizarEtapaChecklistModeloUseCase.ExecutarAsync(request);
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
            var response = await _excluirEtapaChecklistModeloUseCase.ExecutarAsync(new ExcluirEtapaChecklistModeloRequest { Id = id });
            return Ok(response);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
    }
}
