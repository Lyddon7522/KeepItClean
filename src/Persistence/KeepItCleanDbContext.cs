using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class KeepItCleanDbContext : DbContext, IKeepItCleanDbContext
    {
        public DbSet<GarbageLocation> GarbageLocations { get; set; }
    }
}
