using Criosho.Admin.Services.Abstractions;
using Criosho.Admin.Services.Auth;
using Criosho.Admin.Services.Security;
using Microsoft.Extensions.DependencyInjection;

namespace Criosho.Admin.Services.Extensions;

public static class ServicesCollectionExtensions
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<JwtTokenGenerator>();

        return services;
    }
}
