using Microsoft.EntityFrameworkCore; using Microsoft.EntityFrameworkCore.Design;
namespace Leads.Infrastructure.Persistence;
public sealed class LeadsDbContextFactory : IDesignTimeDbContextFactory<LeadsDbContext>
{ public LeadsDbContext CreateDbContext(string[] args){var b=new DbContextOptionsBuilder<LeadsDbContext>(); b.UseNpgsql(Environment.GetEnvironmentVariable("DATABASE_URL")?? "Host=localhost;Port=5432;Database=leads;Username=postgres;Password=Matrix29#;SearchPath=leads;"); return new LeadsDbContext(b.Options);} }
