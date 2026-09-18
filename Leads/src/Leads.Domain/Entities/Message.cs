namespace Leads.Domain.Entities;

public sealed class Message
{
    public long Id
    {
        get; private set;
    }
    public long ConversationId
    {
        get; private set;
    }
    public string SenderType { get; private set; } = "";
    public string Content { get; private set; } = "";
    public DateTime SentAt
    {
        get; private set;
    }
    private Message()
    {
    }
}
