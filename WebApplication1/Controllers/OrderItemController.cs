using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{

    public class OrderItemModel
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int DiscId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

    }
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        public modelsContext Context { get; }

        public OrderItemController(modelsContext context)
        {
            Context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<OrderItem> orderItem = Context.OrderItems.ToList();
            return Ok(orderItem);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            OrderItem? orderItem = Context.OrderItems.FirstOrDefault(x => x.OrderItemId == id);
            if (id <= 0)
            {
                return BadRequest("ID должен быть больше нуля.");
            }
            if (orderItem == null)
            {
                return NotFound("Данных о таком заказе не существует");
            }
            return Ok(orderItem);
        }

        [HttpPost]
        public IActionResult Add(OrderItemModel orderItem)
        {


            var orderItemAdd = new OrderItem()
            {

                OrderItemId = orderItem.OrderItemId,
                OrderId = orderItem.OrderId,
                DiscId = orderItem.DiscId,
                Quantity = orderItem.Quantity,
                Price = orderItem.Price,
            };


            Context.OrderItems.Add(orderItemAdd);
            Context.SaveChanges();
            return Ok(orderItemAdd);

        }

        [HttpPut("{id}")]
        public IActionResult Update(OrderItemModel orderItem,int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID должен быть больше нуля.");
            }

            var orderItemAdd = new OrderItem()
            {

                OrderItemId = orderItem.OrderItemId,
                OrderId = orderItem.OrderId,
                DiscId = orderItem.DiscId,
                Quantity = orderItem.Quantity,
                Price = orderItem.Price,
            };


            Context.OrderItems.Update(orderItemAdd);
            Context.SaveChanges();
            return Ok(orderItemAdd);

        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            OrderItem? orderItem = Context.OrderItems.FirstOrDefault(x => x.OrderItemId == id);
            if (id <= 0)
            {
                return BadRequest("ID должен быть больше нуля.");
            }
            if (orderItem == null)
            {
                return NotFound("Данных о таком заказе не существует");
            }
            Context.OrderItems.Remove(orderItem);
            Context.SaveChanges();
            return Ok(orderItem);
        }
    }
}
