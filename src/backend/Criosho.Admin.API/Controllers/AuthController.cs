using Criosho.Admin.DTOs.Auth;
using Criosho.Admin.DTOs.Common;
using Criosho.Admin.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Criosho.Admin.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(ApiResponse<AuthTokenResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<AuthTokenResponseDto>), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request, CancellationToken cancellationToken)
    {
        var result = await _authService.LoginAsync(request, cancellationToken);

        if (result is null)
        {
            return Unauthorized(ApiResponse<AuthTokenResponseDto>.Fail("Credenciales inválidas o email sin confirmar."));
        }

        return Ok(ApiResponse<AuthTokenResponseDto>.Ok(result, "Login exitoso."));
    }

    [HttpPost("refresh-token")]
    [ProducesResponseType(typeof(ApiResponse<AuthTokenResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<AuthTokenResponseDto>), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto request, CancellationToken cancellationToken)
    {
        var result = await _authService.RefreshTokenAsync(request, cancellationToken);

        if (result is null)
        {
            return Unauthorized(ApiResponse<AuthTokenResponseDto>.Fail("Refresh token inválido o expirado."));
        }

        return Ok(ApiResponse<AuthTokenResponseDto>.Ok(result, "Token refrescado correctamente."));
    }
}
