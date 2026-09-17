namespace Leads.Domain.Entities;
using Leads.Domain.Enums;
public sealed class Lead
{
    public long LeadId { get; private set; }
    public DateTime FechaRegistro { get; private set; }
    public string Canal { get; private set; } = "";
    public long EmpresaId { get; private set; }
    public long PuntoVentaId { get; private set; }
    public string NombreCliente { get; private set; } = "";
    public string? Telefono { get; private set; }
    public string? Email { get; private set; }
    public string? Ciudad { get; private set; }
    public string? ModeloInteresTexto { get; private set; }
    public LeadStatus EstadoGestion { get; private set; }
    public DateTime? FechaPrimerContacto { get; private set; }
    public string? Campania { get; private set; }
    public Guid? CustomerId { get; private set; }
    // Pipeline technical fields: populated by Python normalization/catalog matching.
    public string? TelefonoNormalizado { get; private set; }
    public string? EmailNormalizado { get; private set; }
    public string? CiudadNormalizada { get; private set; }
    public string? CanalNormalizado { get; private set; }
    public string? ModeloTextoNormalizado { get; private set; }
    public string? MotorcycleSku { get; private set; }
    public decimal? ModelMatchConfidence { get; private set; }
    public string? ModelMatchMethod { get; private set; }
    private Lead() { }
    public Lead(long leadId, DateTime fechaRegistro, string canal, long empresaId, long puntoVentaId, string nombreCliente, LeadStatus estadoGestion)
    { LeadId=leadId; FechaRegistro=fechaRegistro; Canal=canal; EmpresaId=empresaId; PuntoVentaId=puntoVentaId; NombreCliente=nombreCliente; EstadoGestion=estadoGestion; }
}
