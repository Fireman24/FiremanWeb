// //Fireman->FiremanApi->HydrantController.cs
// //andreygolubkow Андрей Голубков

using System.Linq;

using FiremanApi2.DataBase;
using FiremanApi2.Model;

using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FiremanApi2.Controllers
{
    [Produces("application/json")]
    [Route("api2/[controller]")]
    [EnableCors("CorsPolicy")]
    public class HydrantController : Controller
    {
        private readonly FireContext _dbContext;

        public HydrantController(FireContext context)
        {
            _dbContext = context;
        }

        [HttpGet("{id}")]
        public IActionResult GetHydrantById(int id)
        {
            var hydrant = _dbContext.Hydrants.Include(h=>h.GpsPoint).FirstOrDefault(h => h.Id == id);
            return Json(hydrant);
        }

        [HttpGet("")]
        public IActionResult GetHydrants()
        {
            return Json(_dbContext.Hydrants.Include(h => h.GpsPoint));
        }

        [HttpPost("")]
        public IActionResult AddHydrant([FromBody] Hydrant hydrant)
        {
            if ( hydrant.GpsPoint != null )
            {
                _dbContext.GpsPoints.Add(hydrant.GpsPoint);
            }

            _dbContext.Hydrants.Add(hydrant);
            _dbContext.SaveChanges();
            return Json(hydrant);
        }

        [HttpPut("{id}")]
        public IActionResult EditHydrant(int id,[FromBody] Hydrant hydrant)
        {
            hydrant.Id = id;
            if (hydrant.GpsPoint != null)
            {
                _dbContext.GpsPoints.Add(hydrant.GpsPoint);
            }
            _dbContext.Hydrants.Update(hydrant);
            _dbContext.SaveChanges();
            return Json(hydrant);
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveHydrant(int id)
        {
            var hydrant = _dbContext.Hydrants.FirstOrDefault(h => h.Id == id);
            _dbContext.Hydrants.Remove(hydrant);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
