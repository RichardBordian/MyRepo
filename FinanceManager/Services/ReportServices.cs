using System.Transactions;
using FinanceManager.common.DTO;
using FinanceManager.Interfaces;
using FinanceManager.Interfaces.Services;

namespace FinanceManager.Services
{
    public class ReportServices(IRepo<Transaction> repo) : IReportService
    {
        public async Task<ReportDTO> DailyReport(DateTime date)
        {
            var temp = await repo.GetDailyReport(date);
            var transactions = temp
                .Select(x => new TransactionDTO()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Date = x.Date,
                    Category = new CategoryDTO() { Name = x.Category.Name, Id = x.CategoryId },
                    Storage = new StorageDTO() { Name = x.Storage.Name, Id = x.StorageId },
                    Price = x.Price,
                    Description = x.Description
                })
                .ToList();

            var totalIncome = transactions
                .Where(x => x.Price > 0)
                .Select(x => x.Price)
                .Sum();

            var totalExpenses = transactions
                .Where(x => x.Price < 0)
                .Select(x => x.Price)
                .Sum();

            return new ReportDTO
            {
                TotalExpenses = totalExpenses,
                TotalIncome = totalIncome,
                Transactions = transactions
            };
        }

        public async Task<ReportDTO> PeriodReport(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
            {
                throw new Exception("End date must be bigger than start date");
            }
            var temp = await repo.GetPeriodReport(startDate, endDate);
            var transactions = temp
                .Select(x => new TransactionDTO()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Date = x.Date,
                    Category = new CategoryDTO()
                    {
                        Name = x.Category.Name,
                        Id = x.CategoryId
                    },
                    Storage = new StorageDTO()
                    {
                        Name = x.Category.Name,
                        Id = x.CategoryId,
                    },
                    Price = x.Price,
                    Description = x.Description
                })
                .ToList();

            var totalIncome = transactions
                .Where(x => x.Price >= 0)
                .Select(x => x.Price)
                .Sum();

            var totalExpenses = transactions
                .Where(x => x.Price < 0)
                .Select(x => x.Price)
                .Sum();

            return new ReportDTO
                { TotalExpenses = totalExpenses, TotalIncome = totalIncome, Transactions = transactions };
        }
    }
}