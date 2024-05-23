using FinanceManager.Services;
using FinanceManager.Controllers;
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

            builder.Services.AddScoped<StorageServices>();
            builder.Services.AddScoped<CategoryServices>();
            builder.Services.AddScoped<ReportServices>();
            builder.Services.AddScoped<TransactionServices>();

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