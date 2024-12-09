using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{


    public class OrderModel
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public bool StatusOrder { get; set; }
        public virtual Customer Customer { get; set; } = null!;
    }

    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public modelsContext Context { get; }

        public OrderController(modelsContext context)
        {
            Context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Order> order = Context.Orders.ToList();
            return Ok(order);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Order? order = Context.Orders.FirstOrDefault(x => x.OrderId == id);
            if (id <= 0)
            {
                return BadRequest("ID должен быть больше нуля.");
            }

            if (order == null)
            {
                return NotFound("Нет такого заказа");
            }
            return Ok(order);
        }

        [HttpPost]
        public IActionResult Add(OrderModel order)
        {


            var orderAdd = new Order()
            {
                OrderId = order.OrderId,
                CustomerId = order.CustomerId,
                OrderDate = order.OrderDate,
                StatusOrder = order.StatusOrder,
            };


            Context.Orders.Add(orderAdd);
            Context.SaveChanges();
            return Ok(orderAdd);

        }

        [HttpPut("{id}")]
        public IActionResult Update(OrderModel order,int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID должен быть больше нуля.");
            }

            var orderUpdate = new Order()
            {
                OrderId = order.OrderId,
                CustomerId = order.CustomerId,
                OrderDate = order.OrderDate,
                StatusOrder = order.StatusOrder,
            };


            Context.Orders.Add(orderUpdate);
            Context.SaveChanges();
            return Ok(orderUpdate);

        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Order? order = Context.Orders.FirstOrDefault(x => x.OrderId == id);
            if (id <= 0)
            {
                return BadRequest("ID должен быть больше нуля.");
            }
          
            if (order == null)
            {
                return NotFound("Такого заказа не существует");
            }

            Context.Orders.Remove(order);
            Context.SaveChanges();
            return Ok(order);
        }
    }
}
