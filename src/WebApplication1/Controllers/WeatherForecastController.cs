using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static List<string> Summaries = new()
        {
            "Дождливо", "Солнечно", "Жарко", "Холодно", "Снежно", "Пасмурно", "Знойно", "Снежно"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("index")]
        public ActionResult<string> Index(int index)
        {
            if (index < 0)
            {
                return BadRequest("Индекс не может быть отрицательным.");
            }

            if (index >= Summaries.Count)
            {
                return NotFound("Результата для такого индекса нет");
            }

            return Ok(Summaries[index]);
        }

        [HttpGet("find-by-name")]

        public IActionResult Get(string name)
        {
            if (name == null)
            {
                return BadRequest("Введите имя");
            }
            int count = Summaries.Count(item => item == name);
            return Ok(count);
        }

        [HttpGet]
        public IActionResult GetAll(int? sortStrategy)
        { List<string> sortedSummaries;

            if (sortStrategy == null)
            {
                sortedSummaries = Summaries;
            }
            else if (sortStrategy == 1)
            {
                sortedSummaries = Summaries.OrderBy(s => s).ToList();
            }
            else if (sortStrategy == -1) 
            {
                sortedSummaries = Summaries.OrderBy(s => s).Reverse().ToList(); 
            }
            else
            {
              return BadRequest("Некорректное значение параметра.");
            }
            return Ok(sortedSummaries);
        }

        [HttpPost]
        public IActionResult Add(string name)
        {
            Summaries.Add(name);
            return Ok();
        }
    }
}
