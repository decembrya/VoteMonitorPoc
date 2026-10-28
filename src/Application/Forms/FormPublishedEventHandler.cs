using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VoteMonitorPoc.Application.Common.Interfaces;
using VoteMonitorPoc.Domain.Events;

namespace VoteMonitorPoc.Application.Forms;

public class FormPublishedEventHandler(IApplicationDbContext context, ILogger<FormPublishedEventHandler> logger)
{
    public async Task Handle(FormPublishedEvent formPublishedEvent, CancellationToken ct)
    {
        var spec = new GetFormByIdSpecification(formPublishedEvent.FormId);

        var form = await context.Forms.WithSpecification(spec).FirstOrDefaultAsync(ct);
        if (form is null)
        {
            logger.LogError($"could not find form with id : {formPublishedEvent.FormId}");
        }

        logger.LogInformation($"form id that was published: {formPublishedEvent.FormId}, with published state: {formPublishedEvent.IsPublished}");
        SentrySdk.Logger.LogInfo("A simple log message");
    }
}
