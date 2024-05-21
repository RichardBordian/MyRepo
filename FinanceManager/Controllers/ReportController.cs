using Microsoft.AspNetCore.Mvc;
using FinanceManager.Services;

namespace FinanceManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private ReportServices _reportServices;

        private ReportController()
        {

        }

        public ReportController(ReportServices reportServices) => _reportServices = reportServices;

        [HttpGet("daily")]
        public ActionResult<Report> DailyReport([FromRoute]DateTime date)
        {
            var result = _reportServices.DailyReport(date);

            return Ok(result);
        }

        [HttpGet("period")]
        public ActionResult<Report> PeriodReport([FromRoute] DateTime from, [FromRoute]DateTime to)
        {
            try
            {
                var result = _reportServices.PeriodReport(from, to);
                return Ok(result);
            }
            catch 
            {
                return BadRequest();
            }
        }
    }
}