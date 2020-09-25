using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class KeepItCleanDbContext : DbContext, IKeepItCleanDbContext
    {
        public KeepItCleanDbContext(DbContextOptions<KeepItCleanDbContext> options)
            : base(options)
        {
        }

        public DbSet<GarbageLocation> GarbageLocations { get; set; }
    }
}
