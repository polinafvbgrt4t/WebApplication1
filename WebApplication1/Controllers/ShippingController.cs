using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ShippingModel
    {
        public int ShippingId { get; set; }
        public int OrderId { get; set; }
        public DateTime ShippingDate { get; set; }
        public string DeliveryServise { get; set; } = null!;
        public string? TrecerShipping { get; set; }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingController : ControllerBase
    {

        public modelsContext Context { get; }

        public ShippingController(modelsContext context)
        {
            Context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Shipping> shipping = Context.Shippings.ToList();
            return Ok(shipping);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Shipping? shipping = Context.Shippings.FirstOrDefault(x => x.ShippingId == id);
            if (id <= 0)
            {
                return BadRequest("ID должен быть больше нуля.");
            }
            if (shipping == null)
            {
                return NotFound("Такой доставки не существует");
            }
            return Ok(shipping);
        }

        [HttpPost]
        public IActionResult Add(ShippingModel shipping)
        {


            var shippingAdd = new Shipping()
            {

                ShippingId = shipping.ShippingId,
                OrderId = shipping.OrderId,
                ShippingDate = shipping.ShippingDate,
                DeliveryServise = shipping.DeliveryServise,
                TrecerShipping = shipping.TrecerShipping,
                
            };


            Context.Shippings.Add(shippingAdd);
            Context.SaveChanges();
            return Ok(shippingAdd);

        }

        [HttpPut("{id}")]
        public IActionResult Update(ShippingModel shipping,int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID должен быть больше нуля.");
            }

            var shippingUpdate = new Shipping()
            {

                ShippingId = shipping.ShippingId,
                OrderId = shipping.OrderId,
                ShippingDate = shipping.ShippingDate,
                DeliveryServise = shipping.DeliveryServise,
                TrecerShipping = shipping.TrecerShipping,
                
            };


            Context.Shippings.Update(shippingUpdate);
            Context.SaveChanges();
            return Ok(shippingUpdate);

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            Shipping? shipping = Context.Shippings.FirstOrDefault(x => x.ShippingId == id);
            if (id <= 0)
            {
                return BadRequest("ID должен быть больше нуля.");
            }
            if (shipping == null)
            {
                return NotFound("Такой доставки не существует");
            }
            Context.Shippings.Remove(shipping);
            Context.SaveChanges();
            return Ok(shipping);
        }
    }
}
