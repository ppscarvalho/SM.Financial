#nullable disable

using Microsoft.EntityFrameworkCore;
using SM.Financial.Core.Application.Interfaces.Repositories.Domain;
using SM.Financial.Core.Domain.Entities;
using SM.Financial.Infrastructure.DbContexts;
using SM.Resource.Data;

namespace SM.Financial.Infrastructure.Repositories
{
    public class BillToPayRepository : IBillToPayRepository
    {
        private readonly FinancialDbContext _context;
        private bool disposedValue;

        public BillToPayRepository(FinancialDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<BillToPay>> GetAllBillToPay()
        {
            return await _context.BillToPay.AsNoTracking().OrderBy(c => c.Description).ToListAsync();
        }

        public async Task<BillToPay> GetBillToPayById(Guid id)
        {
            return await _context.BillToPay.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<BillToPay> AddBillToPay(BillToPay BillToPay)
        {
            return (await _context.AddAsync(BillToPay)).Entity;
        }

        public async Task<BillToPay> UpdateBillToPay(BillToPay BillToPay)
        {
            await Task.CompletedTask;
            _context.Entry(BillToPay).State = EntityState.Modified;
            return BillToPay;
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
