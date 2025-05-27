using Microsoft.AspNetCore.Mvc;
using FinanceManager.Services;
using FinanceManager.common.DTO;
using FinanceManager.Interfaces.Services;

namespace FinanceManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController(IReportService reportService) : ControllerBase
    {

        [HttpGet("{date}")]
        public ActionResult<ReportDTO> DailyReport([FromRoute] DateTime date)
        {
            var result = reportService.DailyReport(date);

            return Ok(result);
        }

        [HttpGet("{from}/{to}")]
        public ActionResult<ReportDTO> PeriodReport([FromRoute] DateTime from, [FromRoute] DateTime to)
        {
            try
            {
                var result = reportService.PeriodReport(from, to);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}