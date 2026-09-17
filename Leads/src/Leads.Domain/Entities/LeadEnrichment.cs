namespace Leads.Domain.Entities;
public sealed class LeadEnrichment
{
    public long Id { get; private set; }
    public long LeadId { get; private set; }
    public string? ModeloInteres { get; private set; }
    public decimal? CuotaInicial { get; private set; }
    public string? FormaPago { get; private set; }
    public string? Intencion { get; private set; }
    public string? Objecion { get; private set; }
    public bool SolicitoCita { get; private set; }
    public bool SolicitoCotizacion { get; private set; }
    public decimal Confianza { get; private set; }
    public string Provider { get; private set; } = "";
    public string PipelineVersion { get; private set; } = "";
    public DateTime CreatedAt { get; private set; }
    private LeadEnrichment() { }
}
