using Ardalis.Result;
using VoteMonitorPoc.Application.Common.Interfaces;
using VoteMonitorPoc.Domain.Entities;

namespace VoteMonitorPoc.Application.Forms.Create;

public class CreateFormCommandHandler(IApplicationDbContext context)
{
    public async Task<Result<Form>> Handle(CreateFormCommand command, CancellationToken ct)
    {
        Form form = new Form(command.FormId);
        if (command.IsPublished)
        {
            form.Publish();
        }
        context.Forms.Add(form);

        //to force domain events to be sent after we change the entity
        await context.SaveChangesAsync(ct);

        Console.WriteLine($"form id created: {form.Id}");

        //best practice to return a mapped DTO, but keeping it simple for PoC purposes
        return Result.Created(form);
    }
}
