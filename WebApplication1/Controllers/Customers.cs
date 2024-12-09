using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication1.Controllers
{
    public class CustomersModel
    {
        public int CustomerId { get; set; }
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string AddressCustomer { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        public modelsContext Context { get; }

        public CustomersController(modelsContext context)
        {
            Context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Customer> customers = Context.Customers.ToList();

            return Ok(customers);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Customer? customer = Context.Customers.FirstOrDefault(x => x.CustomerId == id);
            if (id <= 0)
            {
                return BadRequest("ID должен быть больше нуля.");
            }

            if (customer == null)
            {
                return NotFound("Такого пользователя нет");
            }
            return Ok(customer);
        }

        [HttpPost]
        public IActionResult Add(CustomersModel customer)
        {
          

            var customerAdd = new Customer()
            {
                CustomerId = customer.CustomerId,
                NameSurname = customer.NameSurname,
                AddressCustomer = customer.AddressCustomer,
                Email = customer.Email,
            };

          
                Context.Customers.Add(customerAdd);
                Context.SaveChanges();
                 return Ok(customerAdd);
          
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, CustomersModel customer)
        {

            if (id <= 0)
            {
                return BadRequest("ID должен быть больше нуля.");
            }


            var customerUpdate = new Customer()
            {
                CustomerId = customer.CustomerId,
                NameSurname = customer.NameSurname,
                AddressCustomer = customer.AddressCustomer,
                Email = customer.Email,
            };


            Context.Customers.Update(customerUpdate);
            Context.SaveChanges();
            return Ok(customerUpdate);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID должен быть больше нуля.");
            }

            Customer? customer = Context.Customers.FirstOrDefault(x => x.CustomerId == id);

            if (customer == null)
            {
                return NotFound("Пользователь не найден для удаления.");
            }

            Context.Customers.Remove(customer);
            Context.SaveChanges();
            return Ok(customer);
        }
    }
}