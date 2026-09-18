namespace Leads.Domain.Entities;

public sealed class Customer
{
    public Guid CustomerId
    {
        get; private set;
    }
    public long EmpresaId
    {
        get; private set;
    }
    public string Nombre { get; private set; } = "";
    public string? TelefonoNormalizado
    {
        get; private set;
    }
    public string? EmailNormalizado
    {
        get; private set;
    }
    public string? Ciudad
    {
        get; private set;
    }
    private Customer()
    {
    }
}
