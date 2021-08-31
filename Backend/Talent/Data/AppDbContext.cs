using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Talent.Data.Entities;

namespace Talent.Data
{
    public class AppDbContext : IdentityDbContext<AppUser, IdentityRole, string>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Aggregate> Aggregates { get; set; }
        public DbSet<Aggregation> Aggregations { get; set; }
        public DbSet<DataSource> DataSources { get; set; }
        public DbSet<Filter> Filters { get; set; }
        public DbSet<Join> Joins { get; set; }
        public DbSet<Pipeline> Pipelines { get; set; }
        public DbSet<PipelineProcess> PipelineProcesses { get; set; }
        public DbSet<TempDataSource> TempDataSources { get; set; }
        public DbSet<DataList> DataSourcesList { get; set; }
    }
}
