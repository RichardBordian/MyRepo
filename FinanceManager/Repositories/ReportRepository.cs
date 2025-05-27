using FinanceManager.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Repositories
{
    public class ReportRepository
    {
        private readonly Context _context;

        private ReportRepository() { }

        public ReportRepository(Context context) => _context = context;

        public List<Transaction> GetDaily(DateTime date)
        {
            return _context.Transactions
                .Include(s=> s.Storage)
                .Include(c=> c.Category)
                .Where(x => x.Date.Date == date.Date)
                .ToList();
        }

        public List<Transaction> GetPeriod(DateTime start, DateTime end)
        {
            return _context.Transactions
                .Include(s => s.Storage)
                .Include(c => c.Category)
                .Where(x => x.Date.Date >= start.Date && x.Date.Date <= end.Date)
                .ToList();
        }
           
    }
}
