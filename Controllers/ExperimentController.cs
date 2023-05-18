using AB_testsABP.Entitys;
using AB_testsABP.Services;
using Microsoft.AspNetCore.Mvc;

namespace AB_testsABP.Controllers
{
    [ApiController]
    [Route("experiment")]
    public class ExperimentController : Controller
    {
        //Temporarily
        readonly IDatabaseManager _databaseManager;
        public ExperimentController(IDatabaseManager databaseManager)
        {
            this._databaseManager = databaseManager;
        }

        [HttpGet("button_color")]
        public IActionResult GetExpirementButtonColor([FromQuery(Name = "device-token")] string deviceToken)
        {
           
            return Json("Hello");
        }
        [HttpGet("price")]
        public IActionResult GetExpirementPrice([FromQuery(Name = "device-token")] string deviceToken)
        {
            return Json("Hello");
        }
    }
}
