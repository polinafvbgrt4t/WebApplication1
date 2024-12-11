using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebApplication1.Controllers
{
    public class WeatherData
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public int Degree { get; set; }
        public string Location { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class WeatherController : ControllerBase
    {
        private static List<string> Summaries = new()
        {
            "Дождливо", "Солнечно", "Жарко", "Холодно", "Снежно", "Пасмурно", "Знойно", "Снежно"
        };

        public static List<WeatherData> WeatherDatas = new()
        {
            new WeatherData() { Id = 1, Date= "21.01.22", Degree = 10,Location = "Мурманск" },
            new WeatherData() { Id = 2, Date= "01.01.22", Degree = 50,Location = "Феодосия" },
            new WeatherData() { Id = 3, Date= "21.01.22", Degree = 33,Location = "Симферополь" },
            new WeatherData() { Id = 4, Date= "25.01.23", Degree = 38,Location = "Керчь" },
            new WeatherData() { Id = 5, Date= "21.11.22", Degree = 12,Location = "Щелкино" }
        };

        public readonly ILogger<WeatherController> _logger;

        [HttpGet]
        public List<WeatherData> GetAll()
        {
            return WeatherDatas;
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            for (int i = 0; i < WeatherDatas.Count; i++)
            {
                if (WeatherDatas[i].Id == id)
                {
                    return Ok(WeatherDatas[i]);
                }
            }
            return BadRequest("Такой записи не существует");
        }

        [HttpPost]
        public IActionResult Add(WeatherData weatherData)
        {
            if (weatherData.Id < 0)
            {
                return BadRequest("Id не может быть меньше 0");
            }

            for (int i = 0; i < WeatherDatas.Count; i++)
            {
                if (WeatherDatas[i].Id == weatherData.Id)
                {
                    return BadRequest("Запись с таким Id уже существует");
                }
            }
            WeatherDatas.Add(weatherData);
            return Ok();
        }

        [HttpPut]
        public ActionResult Update(WeatherData weatherData)
        {
            if (weatherData.Id < 0)
            {
                return BadRequest("Id не может быть меньше 0");
            }

            for (int i = 0; i < WeatherDatas.Count; i++)
            {
                if (WeatherDatas[i].Id == weatherData.Id)
                {
                    WeatherDatas[i] = weatherData;
                    return Ok();
                }
            }
            return BadRequest("Такая запись не обнаружена");
        }

        [HttpDelete]
        public ActionResult Delete(WeatherData weatherData)
        {
            if (weatherData.Id < 0)
            {
                return BadRequest("Id не может быть меньше 0");
            }

            for (int i = 0; i < WeatherDatas.Count; i++)
            {
                if (WeatherDatas[i].Id == weatherData.Id)
                {
                    WeatherDatas.RemoveAt(i);
                    return Ok();
                }
            }
            return BadRequest("Такая запись не обнаружена");
        }

        [HttpGet("{location}")]
        public ActionResult CheckLocation(string location)
        {
            foreach (var weather in WeatherDatas)
            {
                if (weather.Location.Equals(location, StringComparison.OrdinalIgnoreCase))
                {
                    return Ok("Запись с указанным городом имеется в нашем списке");
                }
            }
            return Ok("Запись с указанным городом не обнаружено");
        }
    }
}
