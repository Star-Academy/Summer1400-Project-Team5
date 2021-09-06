using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Talent.Data.Entities;

namespace Talent.Data
{
    public class AppDbContext : IdentityDbContext<AppUser, IdentityRole, string>
    {
        private readonly SqlConnection _serverConnection;
        public AppDbContext(DbContextOptions options, SqlConnection serverConnection) : base(options)
        {
            _serverConnection = serverConnection;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DataSource>().Property(ds => ds.SqlConnection)
                .HasDefaultValue(_serverConnection);
        }

        public DbSet<Aggregate> Aggregates { get; set; }
        public DbSet<Aggregation> Aggregations { get; set; }
        public DbSet<DataSource> DataSources { get; set; }
        public DbSet<Filter> Filters { get; set; }
        public DbSet<Join> Joins { get; set; }
        public DbSet<Pipeline> Pipelines { get; set; }
        public DbSet<PipelineProcess> PipelineProcesses { get; set; }
        public DbSet<TempDataSource> TempDataSources { get; set; }
    }
}
