using Leads.Domain.Enums;
namespace Leads.Application.DTOs;

public sealed record LeadListItem(long LeadId, DateTime FechaRegistro, string Canal, long EmpresaId, long PuntoVentaId, string NombreCliente, string? Ciudad, string? ModeloInteresTexto, LeadStatus EstadoGestion, LeadPriority? Prioridad, decimal? Score, long? AsesorId);
public sealed record LeadDetail(long LeadId, DateTime FechaRegistro, string Canal, long EmpresaId, long PuntoVentaId, string NombreCliente, string? Telefono, string? Email, string? Ciudad, string? ModeloInteresTexto, LeadStatus EstadoGestion, DateTime? FechaPrimerContacto, string? Campania, string? MotorcycleSku, decimal? ModelMatchConfidence, string? ModelMatchMethod, CustomerSummary? Customer, EnrichmentDto? LatestEnrichment, ScoreDto? LatestScore, AssignmentDto? LatestAssignment);
public sealed record CustomerSummary(Guid CustomerId, string Nombre, string? TelefonoNormalizado, string? EmailNormalizado, string? Ciudad);
public sealed record EnrichmentDto(long LeadId, string? ModeloInteres, decimal? CuotaInicial, string? FormaPago, string? Intencion, string? Objecion, bool SolicitoCita, bool SolicitoCotizacion, decimal Confianza, string Provider, string PipelineVersion, DateTime CreatedAt);
public sealed record ScoreDto(long LeadId, decimal HistoricalProbability, decimal FinalScore, LeadPriority Priority, string ReasonsJson, string ModelVersion, DateTime CreatedAt);
public sealed record AssignmentDto(long LeadId, long AsesorId, DateTime AssignedAt, string Reason);
public sealed record AdvisorDto(long AsesorId, string Nombre, long PuntoVentaId, long EmpresaId, int CapacidadDiariaLeads, bool Activo);
public sealed record MotorcycleDto(string Sku, string Marca, string Linea, int Cilindraje, string Segmento, decimal PrecioLista, int PuntosVentaDisponibles, int UnidadesDisponibles);
public sealed record PipelineRunDto(
    Guid RunId,
    string Status,
    DateTime StartedAt,
    DateTime? FinishedAt,
    int RecordsRead,
    int RecordsProcessed,
    int RecordsFailed,
    string? SummaryJson,
    string? Error
)
{
    public PipelineRunDto(Guid runId, string status, DateTime startedAt, DateTime? finishedAt, string? summaryJson, string? error)
        : this(runId, status, startedAt, finishedAt, 0, 0, 0, summaryJson, error)
    {
    }
}
public sealed record PipelineStartRequest(string? SourceDir = null);
public sealed record PipelineStartResponse(Guid RunId, string Status);
