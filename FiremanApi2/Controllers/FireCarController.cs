// //Fireman->FiremanApi->FireCarController.cs
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
    public class FireCarController : Controller
    {
        private readonly FireContext _dbContext;

        public FireCarController(FireContext context)
        {
            _dbContext = context;
        }

        [HttpGet("")]
        public IActionResult GetFireCars()
        {
            var firecars = _dbContext.FireCars.Include(f => f.Department).Include(f => f.Broadcast).Include(f => f.Fire)
                    .Include(f => f.GpsPoint);

            return Json(firecars);
        }

        [HttpGet("{id}")]
        public IActionResult GetFireCarById(int id)
        {
            var firecars = _dbContext.FireCars.Include(f => f.Department).Include(f => f.Broadcast).Include(f => f.Fire)
                    .Include(f => f.GpsPoint);
            return Json(firecars.FirstOrDefault(f => f.Id == id));
        }

        [HttpPost("")]
        public IActionResult AddFireCar([FromBody] FireCar fireCar)
        {
            fireCar.Department = _dbContext.Departments.FirstOrDefault(d => d.Id == fireCar.Department.Id);
            if (fireCar.GpsPoint != null)
            {
                _dbContext.GpsPoints.Add(fireCar.GpsPoint);
            }
            _dbContext.FireCars.Add(fireCar);
            _dbContext.SaveChanges();
            return Json(fireCar);
        }

        [HttpPut("{id}")]
        public IActionResult EditFireCar(int id,[FromBody] FireCar fireCar)
        {
            fireCar.Id = id;
            if ( fireCar.Department != null )
            {
                fireCar.Department = _dbContext.Departments.FirstOrDefault(d => d.Id == fireCar.Department.Id);
            }
            if (fireCar.GpsPoint != null)
            {
                _dbContext.GpsPoints.Update(fireCar.GpsPoint);
            }
            _dbContext.FireCars.Update(fireCar);
            _dbContext.SaveChanges();
            return Json(fireCar);
        }

        [HttpDelete("{id}")]
        public IActionResult DeactiveteFireCar(int id)
        {
            var car = _dbContext.FireCars.FirstOrDefault(c => c.Id == id);
            if ( car != null )
            {
                car.Active = false;
            }
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
