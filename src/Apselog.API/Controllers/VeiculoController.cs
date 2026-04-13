using Apselog.Application.DTOs.Request.Veiculo;
using Apselog.Application.UseCases.Interfaces.Veiculo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Apselog.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class VeiculoController : ControllerBase
{
    private readonly ICriarVeiculoUseCase _criarVeiculoUseCase;
    private readonly IAtualizarVeiculoUseCase _atualizarVeiculoUseCase;
    private readonly IListarVeiculoUseCase _listarVeiculoUseCase;
    private readonly IExcluirVeiculoUseCase _excluirVeiculoUseCase;

    public VeiculoController(
        ICriarVeiculoUseCase criarVeiculoUseCase,
        IAtualizarVeiculoUseCase atualizarVeiculoUseCase,
        IListarVeiculoUseCase listarVeiculoUseCase,
        IExcluirVeiculoUseCase excluirVeiculoUseCase)
    {
        _criarVeiculoUseCase = criarVeiculoUseCase;
        _atualizarVeiculoUseCase = atualizarVeiculoUseCase;
        _listarVeiculoUseCase = listarVeiculoUseCase;
        _excluirVeiculoUseCase = excluirVeiculoUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> CriarAsync([FromBody] CriarVeiculoRequest request)
    {
        try
        {
            var response = await _criarVeiculoUseCase.ExecutarAsync(request);
            return CreatedAtAction(nameof(CriarAsync), new { id = response.Id }, response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> ListarAsync([FromQuery] ListarVeiculoRequest request)
    {
        try
        {
            var response = await _listarVeiculoUseCase.ExecutarAsync(request);
            return Ok(response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> AtualizarAsync(Guid id, [FromBody] AtualizarVeiculoRequest request)
    {
        try
        {
            request.Id = id;

            var response = await _atualizarVeiculoUseCase.ExecutarAsync(request);
            return Ok(response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
        catch (InvalidOperationException ex)
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
            var response = await _excluirVeiculoUseCase.ExecutarAsync(new ExcluirVeiculoRequest { Id = id });
            return Ok(response);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
    }
}
