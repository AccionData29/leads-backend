using Leads.Application; using Leads.Infrastructure; using Microsoft.OpenApi.Models;
var builder=WebApplication.CreateBuilder(args);
builder.Services.AddApplication(); builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers(); builder.Services.AddEndpointsApiExplorer(); builder.Services.AddSwaggerGen(o=>o.SwaggerDoc("v1",new OpenApiInfo{Title="Leads API",Version="v1"})); builder.Services.AddHealthChecks().AddNpgSql(builder.Configuration.GetConnectionString("Default")??builder.Configuration["DATABASE_URL"]??"");
var app=builder.Build();
if(app.Environment.IsDevelopment()){app.UseSwagger();app.UseSwaggerUI();}
app.UseHttpsRedirection(); app.MapControllers(); app.MapHealthChecks("/api/v1/health"); app.Run();
