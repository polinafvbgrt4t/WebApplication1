using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{

    public class GenreModels
    {

        public int GenreId { get; set; }
        public string DescriptionGenre { get; set; } = null!;
        public string NameGenre { get; set; } = null!;

    }


    [Route("api/[controller]")]
    [ApiController]
    public class GenreControllers : ControllerBase
    {
        public modelsContext Context { get; }

        public GenreControllers(modelsContext context)
        {
            Context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Genre> genre = Context.Genres.ToList();
            return Ok(genre);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)

        {
            if (id <= 0)
            {
                return BadRequest("ID должен быть больше нуля.");
            }

            Genre? genre = Context.Genres.FirstOrDefault(x => x.GenreId == id);
            if (genre == null)
            {
                return NotFound("Такого заказа не существует");
            }
            return Ok(genre);
        }

        [HttpPost]
        public IActionResult Add(GenreModels genre )
        {


            var genreAdd = new Genre()
            {
                GenreId = genre.GenreId,
                DescriptionGenre = genre.DescriptionGenre,
                NameGenre = genre.NameGenre,


            };


            Context.Genres.Add(genreAdd);
            Context.SaveChanges();
            return Ok(genreAdd);

        }

        [HttpPut("{id}")]
        public IActionResult Update(GenreModels genre,int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID должен быть больше нуля.");
            }


            var genreUpdate = new Genre()
            {
                GenreId = genre.GenreId,
                DescriptionGenre = genre.DescriptionGenre,
                NameGenre = genre.NameGenre,


            };


            Context.Genres.Update(genreUpdate);
            Context.SaveChanges();
            return Ok(genreUpdate);

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Genre? genre = Context.Genres.FirstOrDefault(x => x.GenreId == id);
            if (id <= 0)
            {
                return BadRequest("ID должен быть больше нуля.");
            }
            if (genre == null)
            {
                return NotFound("Такой пластинки нет в наличии");
            }

            Context.Genres.Remove(genre);
            Context.SaveChanges();
            return Ok(genre);
        }
    }
}
