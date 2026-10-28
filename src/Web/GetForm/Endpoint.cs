using Ardalis.Result;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using VoteMonitorPoc.Application.Forms.Get;
using VoteMonitorPoc.Domain.Entities;
using Wolverine;

namespace VoteMonitorPoc.Web.GetForm;

public class Endpoint(IMessageBus bus) : Endpoint<Request, Results<Created, ProblemDetails>>
{
    public override void Configure()
    {
        Get("/api/forms/{formId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var query = new GetFormQuery
        {
            FormId = req.FormId
        };
        var result = await bus.InvokeAsync<Result<Form>>(query, ct);

        await Send.Response<Ardalis.Result.IResult, Results<Created, ProblemDetails>>(result, ct: ct);
    }
}
