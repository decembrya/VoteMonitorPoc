using Ardalis.Result;
using Ardalis.Specification.EntityFrameworkCore;
using VoteMonitorPoc.Application.Common.Interfaces;
using VoteMonitorPoc.Domain.Entities;

namespace VoteMonitorPoc.Application.Forms.Get;

public class GetFormQueryHandler(IApplicationDbContext context)
{
    public async Task<Result<Form>> Handle(GetFormQuery query, CancellationToken ct)
    {
        var spec = new GetFormByIdSpecification(query.FormId);

        var form = await context.Forms.WithSpecification(spec).FirstOrDefaultAsync(ct);
        if (form is not null)
        {
            return Result.Success(form);
        }

        return Result.NotFound();
    }
}
