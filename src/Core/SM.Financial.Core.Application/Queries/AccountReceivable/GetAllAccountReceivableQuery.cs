using MediatR;
using SM.Financial.Core.Application.Models;

namespace SM.Financial.Core.Application.Queries.AccountReceivable
{
    public class GetAllAccountReceivableQuery : IRequest<IEnumerable<AccountReceivableModel>>
    {
    }
}
