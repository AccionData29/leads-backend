namespace Leads.Domain.Entities;

public sealed class Motorcycle
{
    public string Sku { get; private set; } = "";
    public string Marca { get; private set; } = "";
    public string Linea { get; private set; } = "";
    public int Cilindraje
    {
        get; private set;
    }
    public string Segmento { get; private set; } = "";
    public decimal PrecioLista
    {
        get; private set;
    }
    public int PuntosVentaDisponibles
    {
        get; private set;
    }
    public int UnidadesDisponibles
    {
        get; private set;
    }
    private Motorcycle()
    {
    }
}
