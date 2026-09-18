namespace Leads.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default")
            ?? configuration["DATABASE_URL"]
            ?? "Host=localhost;Port=5432;Database=leads;Username=postgres;Password=CHANGE_ME_LOCALLY;SearchPath=leads;";

        services.AddDbContext<LeadsDbContext>(options =>
        {
            options.UseNpgsql(connectionString, npgsqlOptions =>
            {
                npgsqlOptions.MigrationsHistoryTable("__EFMigrationsHistory", "leads");
            });
        });

        services.AddScoped<ILeadRepository, LeadRepository>();
        services.AddScoped<IAdvisorRepository, AdvisorRepository>();
        services.AddScoped<IMotorcycleRepository, MotorcycleRepository>();
        services.AddScoped<IPipelineRunRepository, PipelineRunRepository>();

        var pipelineUrl = configuration["Pipeline:BaseUrl"] ?? "http://localhost:8001";
        services.AddHttpClient<IPipelineService, PipelineHttpClient>(client =>
        {
            client.BaseAddress = new Uri(pipelineUrl);
            client.Timeout = TimeSpan.FromSeconds(30);
        });

        return services;
    }
}
