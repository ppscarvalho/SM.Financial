#nullable disable

using Microsoft.EntityFrameworkCore;
using SM.Financial.Core.Application.Interfaces.Repositories.Domain;
using SM.Financial.Core.Domain.Entities;
using SM.Financial.Infrastructure.DbContexts;
using SM.Resource.Data;

namespace SM.Financial.Infrastructure.Repositories
{
    public class AccountReceivableRepository : IAccountReceivableRepository
    {
        private readonly FinancialDbContext _context;
        private bool disposedValue;

        public AccountReceivableRepository(FinancialDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<AccountReceivable>> GetAllAccountReceivable()
        {
            return await _context.AccountReceivable.AsNoTracking().OrderBy(c => c.Description).ToListAsync();
        }

        public async Task<AccountReceivable> GetAccountReceivableById(Guid id)
        {
            return await _context.AccountReceivable.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<AccountReceivable> AddAccountReceivable(AccountReceivable accountReceivable)
        {
            return (await _context.AddAsync(accountReceivable)).Entity;
        }

        public async Task<AccountReceivable> UpdateAccountReceivable(AccountReceivable accountReceivable)
        {
            await Task.CompletedTask;
            _context.Entry(accountReceivable).State = EntityState.Modified;
            return accountReceivable;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context?.Dispose();
                }
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
