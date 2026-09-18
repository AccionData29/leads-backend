namespace Leads.Domain.Entities;

public sealed class Conversation
{
    public long Id
    {
        get; private set;
    }
    public string ConversacionId { get; private set; } = "";
    public long LeadId
    {
        get; private set;
    }
    public string Canal { get; private set; } = "";
    public DateTime FechaInicio
    {
        get; private set;
    }
    private Conversation()
    {
    }
}
