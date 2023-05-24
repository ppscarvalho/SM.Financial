using MediatR;
using SM.Financial.Core.Application.Models;

namespace SM.Financial.Core.Application.Queries.BillToPay
{
    public class GetAllBillToPayQuery : IRequest<IEnumerable<BillToPayModel>>
    {
    }
}
