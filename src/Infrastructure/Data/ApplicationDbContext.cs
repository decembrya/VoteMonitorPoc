using System.Reflection;
using VoteMonitorPoc.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using VoteMonitorPoc.Domain.Entities;

namespace VoteMonitorPoc.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options), IApplicationDbContext
{
    public DbSet<Form> Forms => Set<Form>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
