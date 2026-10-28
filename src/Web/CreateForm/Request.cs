namespace VoteMonitorPoc.Web.CreateForm;

public record Request
{
    public int FormId { get; set; }

    public bool IsPublished { get; set; }
}
