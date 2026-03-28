using Criosho.Admin.DTOs.Auth;

namespace Criosho.Admin.Services.Abstractions;

public interface IAuthService
{
    Task<AuthTokenResponseDto?> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken = default);
    Task<AuthTokenResponseDto?> RefreshTokenAsync(RefreshTokenRequestDto request, CancellationToken cancellationToken = default);
}
