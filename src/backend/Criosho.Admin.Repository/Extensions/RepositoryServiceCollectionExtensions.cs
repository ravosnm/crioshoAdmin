using Criosho.Admin.Domain.Entities;
using Criosho.Admin.Repository.Abstractions;
using Criosho.Admin.Repository.Data;
using Criosho.Admin.Repository.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Criosho.Admin.Repository.Extensions;

public static class RepositoryServiceCollectionExtensions
{
    public static IServiceCollection AddRepositoryLayer(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' was not found.");

        services.AddDbContext<CrioshoAdminDbContext>(options => options.UseSqlServer(connectionString));

        services
            .AddIdentityCore<AppUser>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
            })
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<CrioshoAdminDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        return services;
    }
}
