using Ardalis.Result;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using VoteMonitorPoc.Application.Forms.Create;
using VoteMonitorPoc.Domain.Entities;
using Wolverine;

namespace VoteMonitorPoc.Web.CreateForm;

public class Endpoint(IMessageBus bus)
    : Endpoint<Request, Results<Created, ProblemDetails>>
{
    public override void Configure()
    {
        Post("/api/forms/{formId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var command = new CreateFormCommand
        {
            FormId = req.FormId,
            IsPublished = req.IsPublished
        };
        var result = await bus.InvokeAsync<Result<Form>>(command, ct);

        await Send.Response<Ardalis.Result.IResult, Results<Created, ProblemDetails>>(result, ct: ct);
    }
}
