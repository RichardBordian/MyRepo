using FinanceManager.Models;

namespace FinanceManager.Services
{
    public class ReportServices
    {
        private readonly Context _context;
        private ReportServices()
        {

        }

        public ReportServices(Context context) => _context = context;

        public Report DailyReport(DateTime date)
        {
            var transactions = _context.Transactions
                .Where(x => x.Date.Date == date.Date)
                .ToList();

            double totalIncome = transactions
                .Where(x => x.Price > 0)
                .Select(x => x.Price)
                .Sum();

            double totalExpences = transactions
                .Where(x => x.Price < 0)
                .Select(x => x.Price)
                .Sum();

            return new Report { TotalExpences = totalExpences, TotalIncome = totalIncome, Transactions = transactions };
        }

        public Report PeriodReport(DateTime startDate, DateTime endDate)
        {
            if(startDate > endDate)
            {
                throw new Exception("End date must be bigger than start date");
            }

            var transactions = _context.Transactions
                .Where(x => x.Date.Date > startDate && x.Date.Date < endDate)
                .ToList();

            double totalIncome = transactions
                .Where(x => x.Price >= 0)
                .Select(x => x.Price)
                .Sum();

            double totalExpences = transactions
                .Where(x => x.Price < 0)
                .Select(x => x.Price)
                .Sum();

            return new Report { TotalExpences = totalExpences, TotalIncome = totalIncome, Transactions = transactions };
        }
    }
}
