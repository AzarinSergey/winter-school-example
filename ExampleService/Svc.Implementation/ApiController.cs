using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Svc.Implementation.Model;

namespace Svc.Implementation
{
    public class ApiController : ControllerBase
    {
        private readonly ServiceDbContext _db;

        public ApiController(ServiceDbContext db)
        {
            _db = db;
        }

        [HttpGet("/ping")]
        public IActionResult Ping() => Ok("PONG!");


        [HttpGet("/get")]
        public IActionResult Get() => Ok(_db.Tasks.ToList());
        
    }
}
