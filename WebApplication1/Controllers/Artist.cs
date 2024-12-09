using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ArtistModel
    {
        public int ArtistId { get; set; }
        public string? Nicneym { get; set; }
        public string? Biography { get; set; }
    }


    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        public modelsContext Context { get; }

        public ArtistController (modelsContext context)
        {
            Context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Artist> artist = Context.Artists.ToList();
            return Ok(artist);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Artist? artist = Context.Artists.FirstOrDefault(x => x.ArtistId == id);
            if (id <= 0)
            {
                return BadRequest("ID должен быть больше нуля.");
            }

            if (artist == null)
            {
                return NotFound("Артист не найден");
            }
            return Ok(artist);
        }

        [HttpPost]
        public IActionResult Add(ArtistModel artist)
        {

          


            var artistAdd = new Artist()
            {
                ArtistId = artist.ArtistId,
                Nicneym = artist.Nicneym,
                Biography = artist.Biography,
                
            };


            Context.Artists.Add(artistAdd);
            Context.SaveChanges();
            return Ok(artistAdd);

        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, ArtistModel artist)
        {

            if (id <= 0) 
            {
                return BadRequest("ID должен быть больше нуля.");
            }

            var artistUpdate = new Artist()
            {
                ArtistId = artist.ArtistId,
                Nicneym = artist.Nicneym,
                Biography = artist.Biography,

            };


            Context.Artists.Update(artistUpdate);
            Context.SaveChanges();
            return Ok(artistUpdate);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0) 
            {
                return BadRequest("ID должен быть больше нуля.");
            }
            Artist? artist = Context.Artists.FirstOrDefault(x => x.ArtistId == id);
            if (artist == null)
            {
                return NotFound("Артист не найден для удаления.");
            }

            Context.Artists.Remove(artist);
            Context.SaveChanges();
            return Ok(artist);
        }
    }

}
