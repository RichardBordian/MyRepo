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

        [HttpGet("{Daily}")]
        public ActionResult<Report> DailyReport([FromRoute]DateTime dateTime)
        {
            var result = _reportServices.DailyReport(dateTime);

            return Ok(result);
        }

        [HttpGet("{Period}")]
        public ActionResult<Report> PeriodReport([FromRoute] DateTime startDate, [FromRoute]DateTime endDate)
        {
            try
            {
                var result = _reportServices.PeriodReport(startDate, endDate);
                return Ok(result);
            }
            catch 
            {
                return BadRequest();
            }
        }
    }
}