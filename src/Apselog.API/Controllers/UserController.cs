using Apselog.Application.DTOs.Request;
using Apselog.Application.DTOs.Response;
using Apselog.Application.UseCases.Interfaces;
using Apselog.Domain.Interfaces.Repositories;
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
    private readonly IUserRepository _userRepository;

    public UserController(
        ICriarUserUseCase criarUserUseCase,
        IAtualizarUserUseCase atualizarUserUseCase,
        IDeletarUserUseCase deletarUserUseCase,
        IUserRepository userRepository)
    {
        _criarUserUseCase = criarUserUseCase;
        _atualizarUserUseCase = atualizarUserUseCase;
        _deletarUserUseCase = deletarUserUseCase;
        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<IActionResult> ListarAsync()
    {
        var users = await _userRepository.GetAllAsync();

        var response = users.Select(user => new UserResponse
        {
            Id = user.Id,
            Nome = user.Nome,
            Email = user.Email,
            Cargo = user.Cargo,
            Instituicao = user.Instituicao,
            Role = user.Role,
            Status = user.Status
        });

        return Ok(response);
    }

    [HttpGet("{id:guid}", Name = "ObterUsuarioPorId")]
    public async Task<IActionResult> ObterPorIdAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user is null)
        {
            return NotFound(new { mensagem = "Usuario nao encontrado." });
        }

        return Ok(new UserResponse
        {
            Id = user.Id,
            Nome = user.Nome,
            Email = user.Email,
            Cargo = user.Cargo,
            Instituicao = user.Instituicao,
            Role = user.Role,
            Status = user.Status
        });
    }

    [HttpPost]
    public async Task<IActionResult> CriarAsync([FromBody] CriarUserRequest request)
    {
        try
        {
            var response = await _criarUserUseCase.ExecutarAsync(request);
            return CreatedAtRoute("ObterUsuarioPorId", new { id = response.Id }, response);
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
