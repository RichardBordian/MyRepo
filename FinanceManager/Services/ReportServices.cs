using FinanceManager.common.DTO;
using FinanceManager.Repositories;

namespace FinanceManager.Services
{
    public class ReportServices
    {
        private readonly ReportRepository _reportRepository;
        private ReportServices() { }

        public ReportServices(ReportRepository reportRepository) => _reportRepository = reportRepository;

        public ReportDTO DailyReport(DateTime date)
        {
            var operations = _reportRepository.GetDaily(date);

            var transactions = operations
                .Select(x => new TransactionDTO()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Date = x.Date,
                    Category = new CategoryDTO() { Name = x.Category.Name, Id = x.Category.Id },
                    Storage = new StorageDTO() { Name = x.Storage.Name, Id = x.Storage.Id },
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

            return new ReportDTO
            { 
                TotalExpenses = totalExpences, 
                TotalIncome = totalIncome, 
                Transactions = transactions };
        }

        public ReportDTO PeriodReport(DateTime startDate, DateTime endDate)
        {
            if(startDate > endDate)
            {
                throw new Exception("End date must be bigger than start date");
            }

            var operations = _reportRepository.GetPeriod(startDate, endDate);

            var transactions = operations
                .Select(x => new TransactionDTO()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Date = x.Date,
                    Category = new CategoryDTO() { Name = x.Category.Name, Id = x.Category.Id},
                    Storage = new StorageDTO() { Name = x.Storage.Name, Id = x.Storage.Id },
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

            return new ReportDTO { TotalExpenses = totalExpences, TotalIncome = totalIncome, Transactions = transactions };
        }
    }
}
