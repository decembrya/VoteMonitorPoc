using VoteMonitorPoc.Domain.Entities;

namespace VoteMonitorPoc.Domain.Events;

public class FormPublishedEvent(Form form) : BaseEvent
{
    public int FormId { get; set; } = form.Id;
    public bool IsPublished { get; set; } = form.IsPublished;
}
