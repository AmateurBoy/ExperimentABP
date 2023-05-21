using ExperimentABP.Data;
using ExperimentABP.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
        /// <summary>
        /// Создает статистику по експерементам на основе данных из БД
        /// </summary>
        /// <remarks>Возвращает View</remarks>
        [HttpGet("all")]
        [SwaggerOperation]
        public IActionResult GetAllStatistic()
        {
            var result = determinantService.GetStatistic();
            return View(result);
        }
    }
}
