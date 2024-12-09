using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{

    public class PaymentModel
    {
        public int PaymentId { get; set; }
        public int OrderId { get; set; }
        public string PaymentMethod { get; set; } = null!;
        public decimal Amount { get; set; }

    }

    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {

        public modelsContext Context { get; }

        public PaymentController(modelsContext context)
        {
            Context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Payment> payment = Context.Payments.ToList();
            return Ok(payment);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Payment? payment = Context.Payments.FirstOrDefault(x => x.PaymentId == id);
            if (id <= 0)
            {
                return BadRequest("ID должен быть больше нуля.");
            }
            if (payment == null)
            {
                return NotFound("Такого платежного счета нет");
            }
            return Ok(payment);
        }

        [HttpPost]
        public IActionResult Add(PaymentModel payment)
        {


            var paymentAdd = new Payment()
            {

                PaymentId = payment.PaymentId,
                OrderId = payment.OrderId,
                PaymentMethod = payment.PaymentMethod,
                Amount = payment.Amount,
               
            };


            Context.Payments.Add(paymentAdd);
            Context.SaveChanges();
            return Ok(paymentAdd);

        }

        [HttpPut("{id}")]
        public IActionResult Update(PaymentModel payment,int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID должен быть больше нуля.");
            }

            var paymentUpdate = new Payment()
            {

                PaymentId = payment.PaymentId,
                OrderId = payment.OrderId,
                PaymentMethod = payment.PaymentMethod,
                Amount = payment.Amount,

            };


            Context.Payments.Update(paymentUpdate);
            Context.SaveChanges();
            return Ok(paymentUpdate);

        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Payment? payment = Context.Payments.FirstOrDefault(x => x.PaymentId == id);
            if (id <= 0)
            {
                return BadRequest("ID должен быть больше нуля.");
            }
            if (payment == null)
            {
                return NotFound("Такого платежного счета нет");
            }
            Context.Payments.Remove(payment);
            Context.SaveChanges();
            return Ok(payment);
        }
    }
}
