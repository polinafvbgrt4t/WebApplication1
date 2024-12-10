using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{

    public class DiscModel
    {
        public int DiscId { get; set; }
        public int AlbumId { get; set; }
        public int GenreId { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        
    }

    [Route("api/[controller]")]
    [ApiController]
    public class DiskControllers : ControllerBase
    {

        public modelsContext Context { get; }

        public DiskControllers(modelsContext context)
        {
            Context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Disc> disc = Context.Discs.ToList();
            return Ok(disc);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)


        {
            if (id <= 0)
            {
                return BadRequest("ID должен быть больше нуля.");
            }

            Disc? disc = Context.Discs.FirstOrDefault(x => x.DiscId == id);
            if (disc == null)
            {
                return NotFound("Такой пластинки нет в наличии");
            }
            return Ok(disc);
        }

        [HttpPost]
        public IActionResult Add(DiscModel disc)
        {


            var discAdd = new Disc()
            {
                DiscId = disc.DiscId,
                AlbumId = disc.AlbumId,
                GenreId = disc.GenreId,
                Price = disc.Price,
                StockQuantity = disc.StockQuantity,
             

            };


            Context.Discs.Add(discAdd);
            Context.SaveChanges();
            return Ok(discAdd);

        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, DiscModel disc)
        {
            if (id <= 0)
            {
                return BadRequest("ID должен быть больше нуля.");
            }




            var discUpdate = new Disc()
            {
                DiscId = disc.DiscId,
                AlbumId = disc.AlbumId,
                GenreId = disc.GenreId,
                Price = disc.Price,
                StockQuantity = disc.StockQuantity,
               

            };


            Context.Discs.Add(discUpdate);
            Context.SaveChanges();
            return Ok(discUpdate);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)

        {
            if (id <= 0)
            {
                return BadRequest("ID должен быть больше нуля.");
            }

            Disc? disc = Context.Discs.FirstOrDefault(x => x.DiscId == id);
            if (disc == null)
            {
                return NotFound("Такой пластинки нет в наличии");
            }

            Context.Discs.Remove(disc);
            Context.SaveChanges();
            return Ok(disc);
        }


    }
}
