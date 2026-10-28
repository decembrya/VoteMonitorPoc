using Ardalis.Result;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using VoteMonitorPoc.Application.Forms.Publish;
using VoteMonitorPoc.Domain.Entities;
using Wolverine;

namespace VoteMonitorPoc.Web.PublishForm;

public class Endpoint(IMessageBus bus)
    : Endpoint<Request, Results<Ok, ProblemDetails>>
{
    public override void Configure()
    {
        Patch("/api/forms/{formId}/publish");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var command = new PublishFormCommand { FormId = req.FormId };
        var result = await bus.InvokeAsync<Result<Form>>(command, ct);

        await Send.Response<Ardalis.Result.IResult, Results<Ok, ProblemDetails>>(result, ct: ct);
    }
}
