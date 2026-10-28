namespace VoteMonitorPoc.Domain.Entities;

public class Form : BaseAuditableEntity, IAggregateRoot
{
    public bool IsPublished { get; set; } = false;

    public Form()
    {
        
    }

    public Form(int formId)
    {
        Id = formId;
    }

    public void Publish()
    {
        IsPublished = true;

        AddDomainEvent(new FormPublishedEvent(this));
    }
}
