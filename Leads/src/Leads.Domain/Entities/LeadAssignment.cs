namespace Leads.Domain.Entities;

public sealed class LeadAssignment
{
    public long Id
    {
        get; private set;
    }
    public long LeadId
    {
        get; private set;
    }
    public long AsesorId
    {
        get; private set;
    }
    public DateTime AssignedAt
    {
        get; private set;
    }
    public string Reason { get; private set; } = "";
    private LeadAssignment()
    {
    }
}
