using Microsoft.AspNetCore.Mvc;
using System.Linq;
using BIpower.Models;
using Microsoft.EntityFrameworkCore;

namespace BIpower.Controllers
{
    [Route("api/[controller]")]
    public class OrderController: Controller
    {
        private readonly biContext _db;
        public OrderController(biContext db){
            
            _db = db;

        }
        [HttpGet("{pageNumber:int}/{pageSize:int}")]
        public IActionResult GetOrders(int pageNumber, int pageSize){
            var orders = _db.Orders.Include(c=> c.Customer).OrderByDescending(x=>x.OrderPlaced);
            var page = new Pager<Order>(orders, pageNumber, pageSize);

            double totalPages = page.Total/pageSize;

            var response = new {
                Page = page,
                TotalPages = totalPages
            };
            return Ok(response);
        }
        [HttpGet]
        public IActionResult GroupByProvince(){

            var orders = _db.Orders.Include(c => c.Customer).ToList();
            var orderedList = orders
                .GroupBy(r => r.Customer.Province)
                .ToList()
                .Select(grp => new 
                {
                    Province = grp.Key,
                    Total = grp.Sum(x => x.Total)
                }).OrderByDescending(r => r.Total)
                .ToList();

            return Ok(orderedList);
        }

         [HttpGet("GroupByCustomer/{n}")]
        public IActionResult GroupByCustomer(int n){

            var orders = _db.Orders.Include(c => c.Customer).ToList();
            var orderedList = orders
                .GroupBy(r => r.Customer.Id)
                .ToList()
                .Select(grp => new 
                {
                    Name = _db.Customers.Find(grp.Key).Name,
                    Total = grp.Sum(x => x.Total)
                }).OrderByDescending(r => r.Total)
                .Take(n)
                .ToList();

            return Ok(orderedList);
        }

        [HttpGet("GetOrder/{id}")]
        public IActionResult GetOrder(int id){

            var order = _db.Orders.Include(o => o.Customer).FirstOrDefault(o => o.id==id);
            if(order == null){
                return BadRequest();
            }
            return Ok(order);
        }
        [HttpPost]
        public IActionResult Post([FromBody] Order order){

            if(order==null){
                return BadRequest();
            }
            _db.Orders.Add(order);
            _db.SaveChanges();
            
            return CreatedAtRoute("GetOrder", new {id = order.id}, order);
        }

        [HttpPut]
        public IActionResult Put (int id, [FromBody] Order order){

            if (order == null) {
                return BadRequest();
            }
            var orderToUpdate = _db.Orders.Where(o => o.id == id).FirstOrDefault();

            if(orderToUpdate==null){
                return NotFound();
            }
            
            orderToUpdate.Customer = order.Customer;
            orderToUpdate.OrderCompleted = order.OrderCompleted;
            orderToUpdate.OrderPlaced = order.OrderPlaced;
            orderToUpdate.Total = order.Total;
            
            _db.SaveChanges();
            return NoContent();

        }

        [HttpDelete]
        public IActionResult Delete(int id){

            var order = _db.Orders.Where(o=>o.id==id).FirstOrDefault();
            if(order ==null){
                return BadRequest();
            }
            _db.Orders.Remove(order);
            _db.SaveChanges();
            return NoContent();
        }


        
    }
}