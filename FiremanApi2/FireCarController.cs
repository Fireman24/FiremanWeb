// //FireFight->FireFight->FireCarController.cs
// //andreygolubkow Андрей Голубков

using System;
using System.Collections.Generic;
using System.Linq;

using FiremanApi2.Adapters;
using FiremanApi2.DataBase;
using FiremanApi2.Model;

using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FiremanApi2
{
    [Route("[controller]")]
    [EnableCors("CorsPolicy")]
    public class FireCarController: Controller
    {
        private readonly FireContext _dbContext;
        private readonly IHostingEnvironment _appEnvironment;

        private static List<Broadcast> _videosCache = new List<Broadcast>();

        public FireCarController(FireContext context, IHostingEnvironment appEnvironment)
        {
            _dbContext = context;
            _appEnvironment = appEnvironment;


        }

        [HttpGet("{id}/update")]
        public IActionResult Update(int id,[FromQuery] double lat, [FromQuery] double lon)
        {                                                                                   
            var data = new FirecarData();
            var car = _dbContext.FireCars.Include(c => c.GpsPoint).FirstOrDefault(c => c.Id == id);
            if ( car.GpsPoint == null )
            {
                car.GpsPoint = new GpsPoint();
            }

            car.GpsPoint.Lat = lat;
            car.GpsPoint.Lon = lon;
            _dbContext.SaveChanges();
            
            if ( _dbContext.FireCars.FirstOrDefault(c => c.Id == id).Fire != null )
            {
                var fire = _dbContext.FireCars.Include(c => c.Fire)
                        .ThenInclude(f => f.Images)
                        .FirstOrDefault(c => c.Id == id)
                        .Fire;
                data.FireAvailable = true;
                data.Address = fire.Address;
                data.Description = fire.Comments;
                data.ImagesList = fire.Images;
                data.LogList = new List<string>();
                data.TaskDateTime = fire.StartDateTime;
               _dbContext.GpsLog.Add(new GpsRecord(car, fire, lat, lon));
                _dbContext.SaveChanges();
            }
            else
            {
                data.FireAvailable = false;
                data.TaskDateTime = new DateTime();
                data.Address = "";
                data.Description = "";
                data.ImagesList = new List<Image>();
                data.LogList = new List<string>();
            }
            

            return Json(data);
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

        [HttpGet("{idCar}/images")]
        public IActionResult GetImages(int idCar)
        {
            if (_dbContext.FireCars.Include(c => c.Fire).FirstOrDefault(c => c.Id == idCar).Fire != null )
            {
                var fire = _dbContext.FireCars.Include(c => c.Fire)
                        .ThenInclude(f => f.Images)
                        .FirstOrDefault(c => c.Id == idCar)
                        .Fire;
                var images = fire.Images;
                return Json(images);
            }
            return Json(new List<Image>());
        }



    }
}
