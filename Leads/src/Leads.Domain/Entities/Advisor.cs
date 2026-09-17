namespace Leads.Domain.Entities;
public sealed class Advisor
{
    public long AsesorId { get; private set; }
    public string Nombre { get; private set; } = "";
    public long PuntoVentaId { get; private set; }
    public long EmpresaId { get; private set; }
    public int CapacidadDiariaLeads { get; private set; }
    public bool Activo { get; private set; }
    public DateTime FechaIngreso { get; private set; }
    private Advisor() { }
}
