using Ardalis.Specification;
using VoteMonitorPoc.Domain.Entities;

namespace VoteMonitorPoc.Application.Forms;

public class GetFormByIdSpecification : SingleResultSpecification<Form>
{
    public GetFormByIdSpecification(int formId)
    {
        Query.Where(f => f.Id == formId);
    }
}
