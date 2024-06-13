using FinanceBlazor.Services;
using MudBlazor.Services;

namespace FinanceBlazor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddMudServices();
            builder.Services.AddServerSideBlazor();

            builder.Services.AddSingleton<StorageServices>();
            builder.Services.AddSingleton<CategoryServices>();
            builder.Services.AddSingleton<TransactionServices>();
            builder.Services.AddSingleton<ReportServices>();

            builder.Services.AddHttpClient();

            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}