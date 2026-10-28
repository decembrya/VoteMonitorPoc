using System.Net;
using Ardalis.Result;
using FastEndpoints;

namespace VoteMonitorPoc.Web;

static class ArdalisResultsExtensions
{
    public static async Task Response<TResult, TResponse>(this IResponseSender ep, TResult? result = default, Func<TResult, TResponse>? mapper = default, CancellationToken ct = default) 
        where TResult : Ardalis.Result.IResult
    {
        switch (result.Status)
        {
            case ResultStatus.Ok:
                await ep.HttpContext.Response.SendOkAsync(result.GetValue(), cancellation: ct);
                break;

            case ResultStatus.Created:
                await ep.HttpContext.Response.SendCreatedAtAsync(ep.Definition.EndpointType.FullName, cancellation: ct);
                break;

            case ResultStatus.Invalid:
                foreach (var validationError in result.ValidationErrors)
                {
                    ep.ValidationFailures.Add(new(validationError.Identifier, validationError.ErrorMessage));
                }

                await ep.HttpContext.Response.SendErrorsAsync(ep.ValidationFailures, cancellation: ct);
                break;
        }
    }
}
