using MediatR;
using SM.Financial.Core.Application.Models;

namespace SM.Financial.Core.Application.Queries.AccountReceivable
{
    public class GetAccountReceivableByIdQuery : IRequest<AccountReceivableModel>
    {
        public Guid Id { get; private set; }
        public GetAccountReceivableByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
