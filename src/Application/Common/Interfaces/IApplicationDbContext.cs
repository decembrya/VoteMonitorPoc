using VoteMonitorPoc.Domain.Entities;

namespace VoteMonitorPoc.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<Form> Forms { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
