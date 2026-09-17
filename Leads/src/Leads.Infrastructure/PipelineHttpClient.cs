using System.Net.Http.Json; using Leads.Application.DTOs; using Leads.Application.Ports; using Microsoft.Extensions.Configuration;
namespace Leads.Infrastructure;
public sealed class PipelineHttpClient(HttpClient http) : IPipelineService
{
 public async Task<PipelineStartResponse> StartAsync(PipelineStartRequest request,CancellationToken ct)
 {
  using var response=await http.PostAsJsonAsync("/api/v1/pipeline/runs",request,ct);
  var body=await response.Content.ReadAsStringAsync(ct);
  if(!response.IsSuccessStatusCode) throw new HttpRequestException($"Pipeline returned {(int)response.StatusCode}: {body}");
  return System.Text.Json.JsonSerializer.Deserialize<PipelineStartResponse>(body,new System.Text.Json.JsonSerializerOptions{PropertyNameCaseInsensitive=true}) ?? throw new InvalidOperationException("Invalid pipeline response.");
 }
}
