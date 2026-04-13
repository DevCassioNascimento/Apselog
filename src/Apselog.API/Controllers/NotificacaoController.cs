using Apselog.Application.DTOs.Request.Notificacao;
using Apselog.Application.UseCases.Interfaces.Notificacao;
using Microsoft.AspNetCore.Mvc;

namespace Apselog.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificacaoController : ControllerBase
{
    private readonly ICriarNotificacaoUseCase _criarNotificacaoUseCase;
    private readonly IAtualizarNotificacaoUseCase _atualizarNotificacaoUseCase;
    private readonly IListarNotificacaoUseCase _listarNotificacaoUseCase;
    private readonly IExcluirNotificacaoUseCase _excluirNotificacaoUseCase;

    public NotificacaoController(
        ICriarNotificacaoUseCase criarNotificacaoUseCase,
        IAtualizarNotificacaoUseCase atualizarNotificacaoUseCase,
        IListarNotificacaoUseCase listarNotificacaoUseCase,
        IExcluirNotificacaoUseCase excluirNotificacaoUseCase)
    {
        _criarNotificacaoUseCase = criarNotificacaoUseCase;
        _atualizarNotificacaoUseCase = atualizarNotificacaoUseCase;
        _listarNotificacaoUseCase = listarNotificacaoUseCase;
        _excluirNotificacaoUseCase = excluirNotificacaoUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> CriarAsync([FromBody] CriarNotificacaoRequest request)
    {
        try
        {
            var response = await _criarNotificacaoUseCase.ExecutarAsync(request);
            return CreatedAtAction(nameof(CriarAsync), new { id = response.Id }, response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> ListarAsync([FromQuery] ListarNotificacaoRequest request)
    {
        try
        {
            var response = await _listarNotificacaoUseCase.ExecutarAsync(request);
            return Ok(response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> AtualizarAsync(Guid id, [FromBody] AtualizarNotificacaoRequest request)
    {
        try
        {
            request.Id = id;

            var response = await _atualizarNotificacaoUseCase.ExecutarAsync(request);
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
            var response = await _excluirNotificacaoUseCase.ExecutarAsync(new ExcluirNotificacaoRequest { Id = id });
            return Ok(response);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
    }
}
