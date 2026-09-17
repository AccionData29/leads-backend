using Leads.Application.UseCases; using Microsoft.AspNetCore.Mvc;
namespace Leads.Api.Controllers;
[ApiController][Route("api/v1")]
public sealed class ReferencesController(ReferenceQueries q):ControllerBase
{
 [HttpGet("advisors")] public async Task<IActionResult> Advisors([FromQuery]long? empresaId,[FromQuery]long? puntoVentaId,[FromQuery]bool? activo,CancellationToken ct)=>Ok(await q.AdvisorsAsync(empresaId,puntoVentaId,activo,ct));
 [HttpGet("motorcycles")] public async Task<IActionResult> Motorcycles([FromQuery]string? marca,[FromQuery]string? segmento,CancellationToken ct)=>Ok(await q.MotorcyclesAsync(marca,segmento,ct));
 [HttpGet("pipeline-runs")] public async Task<IActionResult> PipelineRuns([FromQuery]int page=1,[FromQuery]int pageSize=50,CancellationToken ct=default)=>Ok(await q.RunsAsync(page,pageSize,ct));
}
