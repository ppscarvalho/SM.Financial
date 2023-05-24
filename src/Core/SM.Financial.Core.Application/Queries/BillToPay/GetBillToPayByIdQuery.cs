using MediatR;
using SM.Financial.Core.Application.Models;

namespace SM.Financial.Core.Application.Queries.BillToPay
{
    public class GetBillToPayByIdQuery : IRequest<BillToPayModel>
    {
        public Guid Id { get; private set; }

        public GetBillToPayByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
