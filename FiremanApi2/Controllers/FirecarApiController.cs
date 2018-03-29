using System;
using System.Collections.Generic;
using System.Linq;

using FiremanApi2.Adapters;
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
    public class FirecarApiController : Controller
    {
        private readonly FireContext _dbContext;
        
        public FirecarApiController(FireContext context)
        {
            _dbContext = context;
        }
        [HttpGet("{idCar}/fire")]
        public IActionResult GetFire(int idCar)
        {
            var car = _dbContext.FireCars.FirstOrDefault(c => c.Id == idCar);
            if ( car == null )
            {
                return NotFound("Car not found.");
            }
            var firstCarFire = _dbContext.Fires
                            .Include(f=>f.GpsPoint)
                            .Include(f=>f.History)
                            .Include(d=>d.Images)
                            .Include(f => f.FireCars).FirstOrDefault(f => f.FireCars.Any(c=>c.Id == idCar));
            
            return firstCarFire!=null ? (IActionResult)Json(firstCarFire) : NotFound();
        }

        [HttpGet("{idCar}/departure")]
        public IActionResult GetDeparture(int idCar)
        {
            var car = _dbContext.FireCars.FirstOrDefault(c => c.Id == idCar);
            if ( car == null )
            {
                return NotFound("Car not found.");
            }
            var firstCarFire = _dbContext.Departures
                    .Include(d=>d.GpsPoint)
                    .Include(d=>d.History)
                    .Include(d=>d.Images)
                    .Include(d => d.FireCars).FirstOrDefault(f => f.FireCars.Any(c=>c.Id == idCar));
            
            return firstCarFire!=null ? (IActionResult)Json(firstCarFire) : NotFound();
        }

        [HttpPost("{idCar}/location")]
        public IActionResult UpdateLocation(int idCar,[FromBody] GpsPoint gpsPoint)
        {
            var car = _dbContext.FireCars.FirstOrDefault(c => c.Id == idCar);
            if ( car == null )
            {
                return NotFound("Car not found.");
            } 
            _dbContext.GpsPoints.Load();
            car.GpsPoint.Lat = gpsPoint.Lat;
            car.GpsPoint.Lon = gpsPoint.Lon;
            car.LastUpdateTime = DateTime.Now;
            _dbContext.SaveChanges();
            return Ok();
        }
        
        [HttpGet("{idCar}/images")]
        public IActionResult GetImages(int idCar)
        {
            if (_dbContext.FireCars.Include(c => c.Fire).FirstOrDefault(c => c.Id == idCar)?.Fire != null )
            {
                var fire = _dbContext.FireCars.Include(c => c.Fire)
                        .ThenInclude(f => f.Images)
                        .FirstOrDefault(c => c.Id == idCar)
                        ?.Fire;
                var images = fire.Images;
                return Json(images);
            }
            return Json(new List<Image>());
        }
        
        [EnableCors("CorsPolicy")]
        [HttpGet("{carId}/geoobjects")]
        public IActionResult GetGeoObjects(int carId)
        {
            var geoList = new List<GeoObject>();

            if ( _dbContext.FireCars.FirstOrDefault(c => c.Id == carId) != null )
            {
                FireCar car = _dbContext.FireCars.Include(c=>c.Fire).ThenInclude(f=>f.GpsPoint).FirstOrDefault(c => c.Id == carId);
                if ( car.Fire != null )
                {
                    geoList.Add(new GeoObject("fire",car.Fire.GpsPoint,"Пожар",0));
                }
            }

            var hydrants = _dbContext.Hydrants.Where(h => h.Active).Include(o => o.GpsPoint);
            foreach (var hydrant in hydrants)
            {
                if (hydrant.Active == false)
                {
                    continue;
                }
                geoList.Add(new GeoObject("hydrant", hydrant.GpsPoint, "Пожарный гидрант. Дата проверки: " + hydrant.RevisionDate.Date,0));
            }

           
            return Json(geoList);
        }
    }
}
