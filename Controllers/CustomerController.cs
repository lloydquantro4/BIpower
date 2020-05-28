using BIpower.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc;


namespace BIpower.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly biContext _db;
        public CustomerController(biContext db)
        {

            _db = db;

        }

        [HttpGet]
        public IActionResult GetCustomers()
        {

            if (!_db.Customers.Any())
            {
                return NotFound("Customer list is empty");
            }

            var customers = _db.Customers.OrderBy(x => x.Name);
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomer(int id)
        {

            if (!_db.Customers.Any())
            {
                return BadRequest();

            }
            var customer = _db.Customers.Where(x => x.Id == id).FirstOrDefault();
            if (customer == null)
            {
                return NotFound($"Invalid customer id {id}");
            }
            return Ok(customer);
        }
        [HttpPost]
        public IActionResult AddCustomer([FromBody] Customer customer)
        {
            //Ideally use a DTO
            if (customer == null)
            {
                return BadRequest();
            }
            _db.Customers.Add(customer);
            _db.SaveChanges();

            return CreatedAtRoute("GetCustomer", new { id = customer.Id }, customer);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, [FromBody] Customer customer)
        {

            if ((customer == null) || (customer.Id != id))
            {
                return BadRequest();
            }

            var updatedCustomer = _db.Customers.Where(x => x.Id == id).FirstOrDefault();
            if (updatedCustomer == null)
            {
                return NotFound();
            }

            updatedCustomer.Name = customer.Name;
            updatedCustomer.Email = customer.Email;
            updatedCustomer.Province = customer.Province;

            _db.SaveChanges();
            return NoContent();

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            var customer = _db.Customers.Where(x => x.Id == id).FirstOrDefault();
            if (customer == null)
            {
                return NotFound();
            }
            _db.Customers.Remove(customer);
            _db.SaveChanges();
            return NoContent();
        }

    }
}