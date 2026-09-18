using System.Net.Http.Json;
using System.Text.Json;
using Leads.Application.DTOs;
using Leads.Application.Ports;
using Microsoft.Extensions.Configuration;

namespace Leads.Infrastructure;

public sealed class PipelineHttpClient(HttpClient http) : IPipelineService
{
    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public async Task<PipelineStartResponse> StartAsync(PipelineStartRequest request, CancellationToken ct)
    {
        using var response = await http.PostAsJsonAsync("/api/v1/pipeline/runs", request, _jsonOptions, ct);
        var body = await response.Content.ReadAsStringAsync(ct);
        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException($"Pipeline returned {(int)response.StatusCode}: {body}");
        return JsonSerializer.Deserialize<PipelineStartResponse>(body, _jsonOptions)
               ?? throw new InvalidOperationException("Invalid pipeline response.");
    }
}