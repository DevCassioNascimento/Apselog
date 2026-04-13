using Apselog.Application.DTOs.Request;
using Apselog.Application.UseCases.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Apselog.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ICriarUserUseCase _criarUserUseCase;
    private readonly IAtualizarUserUseCase _atualizarUserUseCase;
    private readonly IDeletarUserUseCase _deletarUserUseCase;

    public UserController(
        ICriarUserUseCase criarUserUseCase,
        IAtualizarUserUseCase atualizarUserUseCase,
        IDeletarUserUseCase deletarUserUseCase)
    {
        _criarUserUseCase = criarUserUseCase;
        _atualizarUserUseCase = atualizarUserUseCase;
        _deletarUserUseCase = deletarUserUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> CriarAsync([FromBody] CriarUserRequest request)
    {
        try
        {
            var response = await _criarUserUseCase.ExecutarAsync(request);
            return CreatedAtAction(nameof(CriarAsync), new { id = response.Id }, response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { mensagem = ex.Message });
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> AtualizarAsync(Guid id, [FromBody] AtualizarUserRequest request)
    {
        try
        {
            request.Id = id;

            var response = await _atualizarUserUseCase.ExecutarAsync(request);
            return Ok(response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { mensagem = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeletarAsync(Guid id)
    {
        try
        {
            await _deletarUserUseCase.ExecutarAsync(new DeletarUserRequest { Id = id });
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
    }
}
