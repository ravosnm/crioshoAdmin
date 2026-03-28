using Criosho.Admin.Domain.Entities;
using Criosho.Admin.DTOs.Auth;
using Criosho.Admin.Repository.Abstractions;
using Criosho.Admin.Services.Abstractions;
using Criosho.Admin.Services.Security;
using Microsoft.AspNetCore.Identity;

namespace Criosho.Admin.Services.Auth;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly JwtTokenGenerator _jwtTokenGenerator;

    public AuthService(
        UserManager<AppUser> userManager,
        IRefreshTokenRepository refreshTokenRepository,
        JwtTokenGenerator jwtTokenGenerator)
    {
        _userManager = userManager;
        _refreshTokenRepository = refreshTokenRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<AuthTokenResponseDto?> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            return null;
        }

        var validPassword = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!validPassword || !user.EmailConfirmed)
        {
            return null;
        }

        return await BuildAuthTokenResponseAsync(user, cancellationToken);
    }

    public async Task<AuthTokenResponseDto?> RefreshTokenAsync(RefreshTokenRequestDto request, CancellationToken cancellationToken = default)
    {
        var existingToken = await _refreshTokenRepository.GetByTokenAsync(request.RefreshToken, cancellationToken);

        if (existingToken is null || existingToken.IsRevoked || existingToken.ExpiresAtUtc <= DateTime.UtcNow)
        {
            return null;
        }

        existingToken.IsRevoked = true;
        await _refreshTokenRepository.SaveChangesAsync(cancellationToken);

        return await BuildAuthTokenResponseAsync(existingToken.User, cancellationToken);
    }

    private async Task<AuthTokenResponseDto> BuildAuthTokenResponseAsync(AppUser user, CancellationToken cancellationToken)
    {
        var roles = await _userManager.GetRolesAsync(user);
        var (accessToken, accessTokenExpiresAtUtc) = _jwtTokenGenerator.GenerateAccessToken(user, roles);

        var refreshTokenValue = JwtTokenGenerator.GenerateRefreshToken();
        var refreshTokenExpiresAtUtc = DateTime.UtcNow.AddDays(7);

        await _refreshTokenRepository.AddAsync(new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Token = refreshTokenValue,
            ExpiresAtUtc = refreshTokenExpiresAtUtc
        }, cancellationToken);

        await _refreshTokenRepository.SaveChangesAsync(cancellationToken);

        return new AuthTokenResponseDto
        {
            AccessToken = accessToken,
            AccessTokenExpiresAtUtc = accessTokenExpiresAtUtc,
            RefreshToken = refreshTokenValue,
            RefreshTokenExpiresAtUtc = refreshTokenExpiresAtUtc
        };
    }
}
