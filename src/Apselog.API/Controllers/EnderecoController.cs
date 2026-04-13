using Apselog.Application.DTOs.Request.Endereco;
using Apselog.Application.UseCases.Interfaces.Endereco;
using Microsoft.AspNetCore.Mvc;

namespace Apselog.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnderecoController : ControllerBase
{
    private readonly ICriarEnderecoUseCase _criarEnderecoUseCase;
    private readonly IAtualizarEnderecoUseCase _atualizarEnderecoUseCase;
    private readonly IListarEnderecoUseCase _listarEnderecoUseCase;
    private readonly IExcluirEnderecoUseCase _excluirEnderecoUseCase;

    public EnderecoController(
        ICriarEnderecoUseCase criarEnderecoUseCase,
        IAtualizarEnderecoUseCase atualizarEnderecoUseCase,
        IListarEnderecoUseCase listarEnderecoUseCase,
        IExcluirEnderecoUseCase excluirEnderecoUseCase)
    {
        _criarEnderecoUseCase = criarEnderecoUseCase;
        _atualizarEnderecoUseCase = atualizarEnderecoUseCase;
        _listarEnderecoUseCase = listarEnderecoUseCase;
        _excluirEnderecoUseCase = excluirEnderecoUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> CriarAsync([FromBody] CriarEnderecoRequest request)
    {
        try
        {
            var response = await _criarEnderecoUseCase.ExecutarAsync(request);
            return CreatedAtAction(nameof(CriarAsync), new { id = response.Id }, response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> ListarAsync([FromQuery] ListarEnderecoRequest request)
    {
        try
        {
            var response = await _listarEnderecoUseCase.ExecutarAsync(request);
            return Ok(response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> AtualizarAsync(Guid id, [FromBody] AtualizarEnderecoRequest request)
    {
        try
        {
            request.Id = id;

            var response = await _atualizarEnderecoUseCase.ExecutarAsync(request);
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
            var response = await _excluirEnderecoUseCase.ExecutarAsync(new ExcluirEnderecoRequest { Id = id });
            return Ok(response);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
    }
}
