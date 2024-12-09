using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AlbumModel 
    {
        public int AlbumId { get; set; }
        public int ArtistId { get; set; }
        public string Title { get; set; } = null!;
        public int? GenreId { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {


        public modelsContext Context { get; }

        public AlbumController(modelsContext context)
        {
            Context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Album> album = Context.Albums.ToList();
            return Ok(album);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if (id <= 0) 
            {
                return BadRequest("ID должен быть больше нуля.");
            }

            Album? album = Context.Albums.FirstOrDefault(x => x.AlbumId == id);
            if (album == null)
            {
                return NotFound("Альбом не найден.");
            }
            return Ok(album);
        }

        [HttpPost]
        public IActionResult Add(AlbumModel album)
        {
            if (album.ArtistId <= 0) 
            {
                return BadRequest("ID исполнителя должен быть больше нуля.");
            }


            var albumAdd = new Album()
            {
                AlbumId = album.AlbumId,
                ArtistId = album.ArtistId,
                Title = album.Title,
                GenreId = album.GenreId,
            };


            Context.Albums.Add(albumAdd);
            Context.SaveChanges();
            return Ok(albumAdd);

        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, AlbumModel album)
        {

            if (id <= 0) 
            {
                return BadRequest("ID должен быть больше нуля.");
            }

         
            if (album.ArtistId <= 0) 
            {
                return BadRequest("ID исполнителя должен быть больше нуля.");
            }

            var albumUpdate = new Album()
            {
                AlbumId = album.AlbumId,
                ArtistId = album.ArtistId,
                Title = album.Title,
                GenreId=album.GenreId,
            };


            Context.Albums.Update(albumUpdate);
            Context.SaveChanges();
            return Ok(albumUpdate);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            

            if (id <= 0)
            {
                return BadRequest("ID должен быть больше нуля.");
            }

            Album? album = Context.Albums.FirstOrDefault(x => x.AlbumId == id);
            if (album == null)
            {
                return NotFound("Альбом не найден для удаления.");
            }

            Context.Albums.Remove(album);
            Context.SaveChanges();
            return Ok(album);
        }
    }
}
