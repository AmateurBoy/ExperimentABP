using ExperimentABP.Data;
using ExperimentABP.Entitys;
using ExperimentABP.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
        /// <summary>
        /// Отримати колір кнопки в залежності від device-token
        /// </summary>        
        /// <remarks>Повертае пару ключ,значення з результатом, а саме кольором кнопки в залежносі від device-token в JSON форматі</remarks>
        [HttpGet("button_color")]
        [SwaggerOperation]
        public IActionResult GetExpirementButtonColor([FromQuery(Name = "device-token")] string deviceToken)
        {
            var result = determinantService.QueryExperiment("button_color", deviceToken);
            var KeyValue = new KeyValuePair<string, string>("button_color", result.Name);
            return Json(KeyValue);
        }
        /// <summary>
        /// Отримати ціну в залежності від device-token
        /// </summary>
        /// <remarks>Повертае пару ключ,значення з результатом, а саме ціну в залежносі від device-token в JSON форматі</remarks>
        [HttpGet("price")]
        [SwaggerOperation]
        public IActionResult GetExpirementPrice([FromQuery(Name = "device-token")] string deviceToken)
        {
            var result =  determinantService.QueryExperiment("price", deviceToken);
            var KeyValue = new KeyValuePair<string, string>("price", result.Name);
            return Json(KeyValue);
        }
    }
}
