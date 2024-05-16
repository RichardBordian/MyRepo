using FinanceManager.Models;

namespace FinanceManager.Services
{
    public class ReportServices
    {
        private readonly Context _context = new Context();

        public Report? DailyReport(DateTime date)
        {
            List<Transaction> tranzactions = _context.Transactions.Where(x => x.Date.Date == date.Date).ToList();
            
            double totalIncome = tranzactions
                .Where(x => x.Price > 0)
                .Select(x => x.Price)
                .Sum();
            
            double totalExpences = tranzactions
                .Where(x => x.Price < 0)
                .Select(x => x.Price)
                .Sum();

            return new Report { TotalExpences = totalExpences, TotalIncome = totalIncome, Tranzactions = tranzactions };
        }

        public Report? PeriodReport(DateTime startDate, DateTime endDate)
        {
            List<Transaction> tranzactions = _context.Transactions.Where(x=> x.Date.Date >startDate && x.Date.Date < endDate).ToList();

            double totalIncome = tranzactions
                .Where(x => x.Price > 0)
                .Select(x => x.Price)
                .Sum();

            double totalExpences = tranzactions
                .Where (x => x.Price < 0)
                .Select(x => x.Price)
                .Sum();

            return new Report { TotalExpences = totalExpences, TotalIncome = totalIncome, Tranzactions = tranzactions };
        }
    }
}
