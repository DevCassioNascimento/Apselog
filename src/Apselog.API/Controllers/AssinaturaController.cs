using Apselog.Application.DTOs.Request.Assinatura;
using Apselog.Application.UseCases.Interfaces.Assinatura;
using Microsoft.AspNetCore.Mvc;

namespace Apselog.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssinaturaController : ControllerBase
{
    private readonly ICriarAssinaturaUseCase _criarAssinaturaUseCase;
    private readonly IAtualizarAssinaturaUseCase _atualizarAssinaturaUseCase;
    private readonly IListarAssinaturaUseCase _listarAssinaturaUseCase;
    private readonly IExcluirAssinaturaUseCase _excluirAssinaturaUseCase;

    public AssinaturaController(
        ICriarAssinaturaUseCase criarAssinaturaUseCase,
        IAtualizarAssinaturaUseCase atualizarAssinaturaUseCase,
        IListarAssinaturaUseCase listarAssinaturaUseCase,
        IExcluirAssinaturaUseCase excluirAssinaturaUseCase)
    {
        _criarAssinaturaUseCase = criarAssinaturaUseCase;
        _atualizarAssinaturaUseCase = atualizarAssinaturaUseCase;
        _listarAssinaturaUseCase = listarAssinaturaUseCase;
        _excluirAssinaturaUseCase = excluirAssinaturaUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> CriarAsync([FromBody] CriarAssinaturaRequest request)
    {
        try
        {
            var response = await _criarAssinaturaUseCase.ExecutarAsync(request);
            return CreatedAtAction(nameof(CriarAsync), new { id = response.Id }, response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> ListarAsync([FromQuery] ListarAssinaturaRequest request)
    {
        try
        {
            var response = await _listarAssinaturaUseCase.ExecutarAsync(request);
            return Ok(response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> AtualizarAsync(Guid id, [FromBody] AtualizarAssinaturaRequest request)
    {
        try
        {
            request.Id = id;

            var response = await _atualizarAssinaturaUseCase.ExecutarAsync(request);
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
            var response = await _excluirAssinaturaUseCase.ExecutarAsync(new ExcluirAssinaturaRequest { Id = id });
            return Ok(response);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
    }
}
