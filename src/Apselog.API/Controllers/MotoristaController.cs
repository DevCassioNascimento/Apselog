using Apselog.Application.DTOs.Request.Motorista;
using Apselog.Application.UseCases.Interfaces.Motorista;
using Microsoft.AspNetCore.Mvc;

namespace Apselog.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MotoristaController : ControllerBase
{
    private readonly ICriarMotoristaUseCase _criarMotoristaUseCase;
    private readonly IAtualizarMotoristaUseCase _atualizarMotoristaUseCase;
    private readonly IListarMotoristaUseCase _listarMotoristaUseCase;
    private readonly IExcluirMotoristaUseCase _excluirMotoristaUseCase;

    public MotoristaController(
        ICriarMotoristaUseCase criarMotoristaUseCase,
        IAtualizarMotoristaUseCase atualizarMotoristaUseCase,
        IListarMotoristaUseCase listarMotoristaUseCase,
        IExcluirMotoristaUseCase excluirMotoristaUseCase)
    {
        _criarMotoristaUseCase = criarMotoristaUseCase;
        _atualizarMotoristaUseCase = atualizarMotoristaUseCase;
        _listarMotoristaUseCase = listarMotoristaUseCase;
        _excluirMotoristaUseCase = excluirMotoristaUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> CriarAsync([FromBody] CriarMotoristaRequest request)
    {
        try
        {
            var response = await _criarMotoristaUseCase.ExecutarAsync(request);
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
    }

    [HttpGet]
    public async Task<IActionResult> ListarAsync([FromQuery] ListarMotoristaRequest request)
    {
        try
        {
            var response = await _listarMotoristaUseCase.ExecutarAsync(request);
            return Ok(response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> AtualizarAsync(Guid id, [FromBody] AtualizarMotoristaRequest request)
    {
        try
        {
            request.Id = id;

            var response = await _atualizarMotoristaUseCase.ExecutarAsync(request);
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
            var response = await _excluirMotoristaUseCase.ExecutarAsync(new ExcluirMotoristaRequest { Id = id });
            return Ok(response);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
    }
}
