using SM.Financial.Core.Domain.Entities;
using SM.Resource.Data;

namespace SM.Financial.Core.Application.Interfaces.Repositories.Domain
{
    public interface IAccountReceivableRepository : IRepository<AccountReceivable>
    {
        Task<IEnumerable<AccountReceivable>> GetAllAccountReceivable();
        Task<AccountReceivable> GetAccountReceivableById(Guid id);
        Task<AccountReceivable> AddAccountReceivable(AccountReceivable accountReceivable);
        Task<AccountReceivable> UpdateAccountReceivable(AccountReceivable accountReceivable);
    }
}
