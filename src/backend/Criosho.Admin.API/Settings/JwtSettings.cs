namespace Criosho.Admin.API.Settings;

public sealed class JwtSettings
{
    public string Issuer { get; init; } = "Criosho.Admin";
    public string Audience { get; init; } = "Criosho.Admin.Client";
    public string Key { get; init; } = "ReplaceThisWithASecretKeyAtLeast32Chars";
    public int AccessTokenMinutes { get; init; } = 30;
}
