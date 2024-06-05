using FinanceManager.DTO;

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
                .Select(x => new TransactionDTO()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Date = x.Date,
                    Category = new CategoryDTO() { Name = _context.Categories.FirstOrDefault(c => c.Id == x.CategoryId).Name, Id = _context.Categories.FirstOrDefault(c => c.Id == x.CategoryId).Id },
                    Storage = new StorageDTO() { Name = _context.Storages.FirstOrDefault(c => c.Id == x.StorageId).Name, Id = _context.Storages.FirstOrDefault(c => c.Id == x.StorageId).Id },
                    Price = x.Price,
                    Description = x.Description
                })
                .ToList();

            double totalIncome = transactions
                .Where(x => x.Price > 0)
                .Select(x => x.Price)
                .Sum();

            double totalExpences = transactions
                .Where(x => x.Price < 0)
                .Select(x => x.Price)
                .Sum();

            return new Report
            { 
                TotalExpences = totalExpences, 
                TotalIncome = totalIncome, 
                Transactions = transactions };
        }

        public Report PeriodReport(DateTime startDate, DateTime endDate)
        {
            if(startDate > endDate)
            {
                throw new Exception("End date must be bigger than start date");
            }

            var transactions = _context.Transactions
                .Where(x => x.Date.Date >= startDate && x.Date.Date <= endDate)
                .Select(x => new TransactionDTO()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Date = x.Date,
                    Category = new CategoryDTO() { Name = _context.Categories.FirstOrDefault(c => c.Id == x.CategoryId).Name, Id = _context.Categories.FirstOrDefault(c => c.Id == x.CategoryId).Id },
                    Storage = new StorageDTO() { Name = _context.Storages.FirstOrDefault(c => c.Id == x.StorageId).Name, Id = _context.Storages.FirstOrDefault(c => c.Id == x.StorageId).Id },
                    Price = x.Price,
                    Description = x.Description
                })
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
