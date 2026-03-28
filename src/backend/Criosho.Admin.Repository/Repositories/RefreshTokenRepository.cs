using Criosho.Admin.Domain.Entities;
using Criosho.Admin.Repository.Abstractions;
using Criosho.Admin.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace Criosho.Admin.Repository.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly CrioshoAdminDbContext _dbContext;

    public RefreshTokenRepository(CrioshoAdminDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default)
    {
        await _dbContext.RefreshTokens.AddAsync(refreshToken, cancellationToken);
    }

    public async Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default)
    {
        return await _dbContext.RefreshTokens
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Token == token, cancellationToken);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}
