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
            var result = determinantService.QueryExperiment("button_color", deviceToken);
            var KeyValue = new KeyValuePair<string, string>("button_color", result.Name);
            return Json(KeyValue);
        }
        [HttpGet("price")]
        public IActionResult GetExpirementPrice([FromQuery(Name = "device-token")] string deviceToken)
        {
            var result =  determinantService.QueryExperiment("price", deviceToken);
            var KeyValue = new KeyValuePair<string, string>("price", result.Name);
            return Json(KeyValue);
        }
    }
}
