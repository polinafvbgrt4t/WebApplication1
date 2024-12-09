using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static List<string> Summaries = new()
        {
            "��������", "��������", "�����", "�������", "������", "��������", "������", "������"
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
                return BadRequest("������ �� ����� ���� �������������.");
            }

            if (index >= Summaries.Count)
            {
                return NotFound("���������� ��� ������ ������� ���");
            }

            return Ok(Summaries[index]);
        }

        [HttpGet("find-by-name")]

        public IActionResult Get(string name)
        {
            if (name == null)
            {
                return BadRequest("������� ���");
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
              return BadRequest("������������ �������� ���������.");
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
