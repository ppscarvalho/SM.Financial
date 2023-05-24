using SM.Financial.Core.Domain.Entities;
using SM.Resource.Data;

namespace SM.Financial.Core.Application.Interfaces.Repositories.Domain
{
    public interface IBillToPayRepository : IRepository<BillToPay>
    {
        Task<IEnumerable<BillToPay>> GetAllBillToPay();
        Task<BillToPay> GetBillToPayById(Guid id);
        Task<BillToPay> AddBillToPay(BillToPay bill);
        Task<BillToPay> UpdateBillToPay(BillToPay bill);
    }
}
