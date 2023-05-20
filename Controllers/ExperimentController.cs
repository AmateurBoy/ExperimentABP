using ExperimentABP.Data;
using ExperimentABP.Entitys;
using ExperimentABP.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExperimentABP.Controllers
{
    [ApiController]
    [Route("experiment")]
    public class ExperimentController : Controller
    {
        readonly IDeterminantService determinantService;   
       
        public ExperimentController(IDeterminantService determinantService, IDefaultCreator creator)
        {
            this.determinantService = determinantService;            
        }

        [HttpGet("button_color")]
        public IActionResult GetExpirementButtonColor([FromQuery(Name = "device-token")] string deviceToken)
        {
            Option option = determinantService.QueryExperiment("button_color", deviceToken);
            return Json(option);
        }
        [HttpGet("price")]
        public IActionResult GetExpirementPrice([FromQuery(Name = "device-token")] string deviceToken)
        {
            determinantService.GetStatistic();
            return Json("Hello");
        }
    }
}
