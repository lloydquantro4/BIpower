using System.Linq;
using BIpower.Models;
using Microsoft.AspNetCore.Mvc;


namespace BIpower.Controllers
{
    [Route("api/[controller]")]
    public class ServerController : Controller
    {
        private readonly biContext _db;
        public ServerController(biContext db)
        {

            _db = db;
        }
        [HttpGet]
        public IActionResult GetServers()
        {

            if (!_db.Servers.Any())
            {
                return BadRequest();
            }

            var servers = _db.Servers.ToList();
            return Ok(servers);

        }
        [HttpGet("GetServer/{id}", Name = "GetServer")]
        public IActionResult GetServer(int id)
        {

            var server = _db.Servers.Where(s => s.Id == id).FirstOrDefault();
            if (server == null)
            {
                return BadRequest();
            }
            return Ok(server);
        }
        [HttpPost]
        public IActionResult Post([FromBody] Server server)
        {

            if (server == null)
            {
                return NotFound();
            }
            //valid checks
            _db.Servers.Add(server);
            _db.SaveChanges();

            return CreatedAtRoute("GetServer", new { id = server.Id }, server);

        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ServerMessage message)
        {

            var server = _db.Servers.Where(s => s.Id == id).FirstOrDefault();
            if (server == null)
            {
                return NotFound();
            }
            if (message.Payload == "Activate")
            {
                server.isOnline = true;

            }
            if (message.Payload == "deactivate")
            {
                server.isOnline = false;
            }
            _db.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            var server = _db.Servers.Find(id);
            if (server == null)
            {
                return BadRequest();
            }
            _db.Servers.Remove(server);
            _db.SaveChanges();
            return NoContent();
        }
    }
}