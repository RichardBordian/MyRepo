using Microsoft.AspNetCore.Mvc;
using FinanceManager.Models;
using FinanceManager.Services;

namespace FinanceManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private ReportServices _reportServices = new ReportServices();

        [HttpGet("{Date}")]
        public ActionResult<Report> DailyReport(DateTime dateTime)
        {
            var result = _reportServices.DailyReport(dateTime);

            return result == null ? NotFound() : Ok(result);
        }

        [HttpGet]
        public ActionResult<Report> PeriodReport(DateTime startDate, DateTime endDate)
        {
            var result = _reportServices.PeriodReport(startDate, endDate);

            return result == null ? NotFound() : Ok(result);
        }
    }
}