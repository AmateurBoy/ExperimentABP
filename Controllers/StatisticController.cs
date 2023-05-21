using ExperimentABP.Data;
using ExperimentABP.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExperimentABP.Controllers
{
    [ApiController]
    [Route("statistic")]
    public class StatisticController:Controller
    {
        readonly IDeterminantService determinantService;
        public StatisticController(IDeterminantService determinantService)
        {
            this.determinantService = determinantService;
        }
        [HttpGet("all")]
        public IActionResult GetAllStatistic()
        {
            var result = determinantService.GetStatistic();
            return View(result);
        }
    }
}
