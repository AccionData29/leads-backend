namespace Leads.Domain.Entities;

public sealed class PipelineRun
{
    public Guid RunId
    {
        get; private set;
    }
    public string Status { get; private set; } = "";
    public string PipelineVersion { get; private set; } = "";
    public int RecordsRead
    {
        get; private set;
    }
    public int RecordsProcessed
    {
        get; private set;
    }
    public int RecordsFailed
    {
        get; private set;
    }
    public DateTime StartedAt
    {
        get; private set;
    }
    public DateTime? FinishedAt
    {
        get; private set;
    }
    public string? SummaryJson
    {
        get; private set;
    }
    public string? Error
    {
        get; private set;
    }
    private PipelineRun()
    {
    }
}
