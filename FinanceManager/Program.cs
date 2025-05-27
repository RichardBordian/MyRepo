using FinanceManager.Interfaces;
using FinanceManager.Interfaces.Services;
using FinanceManager.Models;
using FinanceManager.Repos;
using FinanceManager.Services;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connection = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<Context>(options => options.UseSqlServer(connection));

            builder.Services.AddScoped<IStorageServices, StorageServices>();
            builder.Services.AddScoped<ICategorySerivces, CategoryServices>();
            builder.Services.AddScoped<ITransactionServices, TransactionServices>();
            builder.Services.AddScoped<IRepo<Transaction>, TransactionRepo>();
            builder.Services.AddScoped<IRepo<Category>, CategoryRepo>();
            builder.Services.AddScoped<IRepo<Storage>, StorageRepo>();
            builder.Services.AddScoped<IReportService, ReportServices>();
            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}