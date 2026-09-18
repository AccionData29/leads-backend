namespace Leads.Domain.Entities;

using Leads.Domain.Enums;
public sealed class LeadScore
{
    public long Id
    {
        get; private set;
    }
    public long LeadId
    {
        get; private set;
    }
    public decimal HistoricalProbability
    {
        get; private set;
    }
    public decimal FinalScore
    {
        get; private set;
    }
    public LeadPriority Priority
    {
        get; private set;
    }
    public string ReasonsJson { get; private set; } = "[]";
    public string ModelVersion { get; private set; } = "";
    public DateTime CreatedAt
    {
        get; private set;
    }
    private LeadScore()
    {
    }
}
