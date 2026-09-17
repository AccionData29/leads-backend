using Microsoft.Extensions.DependencyInjection; using Leads.Application.UseCases;
namespace Leads.Application;
public static class DependencyInjection { public static IServiceCollection AddApplication(this IServiceCollection services){ services.AddScoped<LeadQueries>(); services.AddScoped<ReferenceQueries>(); services.AddScoped<PipelineCommands>(); return services; } }
