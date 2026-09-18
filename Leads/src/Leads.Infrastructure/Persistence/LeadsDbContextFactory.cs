namespace Leads.Infrastructure.Persistence;

public sealed class LeadsDbContextFactory : IDesignTimeDbContextFactory<LeadsDbContext>
{
    public LeadsDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<LeadsDbContext>();
        optionsBuilder.UseNpgsql(
            Environment.GetEnvironmentVariable("DATABASE_URL")
            ?? "Host=localhost;Port=5432;Database=leads;Username=postgres;Password=CHANGE_ME_LOCALLY;SearchPath=leads;");

        return new LeadsDbContext(optionsBuilder.Options);
    }
}
