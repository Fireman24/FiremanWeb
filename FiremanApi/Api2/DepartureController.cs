// //Fireman->FiremanApi->DepartureController.cs
// //andreygolubkow Андрей Голубков

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using FiremanApi.DataBase;

using FiremanModel;

using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FiremanApi.Api2
{
    [Produces("application/json")]
    [Route("api2/[controller]")]
    [EnableCors("CorsPolicy")]
    public class DepartureController : Controller
    {
        private readonly FireContext _dbContext;
        private readonly IHostingEnvironment _appEnvironment;


        public DepartureController(FireContext context, IHostingEnvironment appEnvironment)
        {
            _dbContext = context;
            _appEnvironment = appEnvironment;
        }

        [HttpGet("{id}")]
        public IActionResult GetDeparture(int id)
        {
            var deps = _dbContext.Departures
                            .Include(f => f.FireCars)
                            .Include(f => f.GpsPoint)
                            .Include(f => f.Operator)
                            .Include(f => f.History)
                            .Include(f => f.Images);
            return Json(deps.FirstOrDefault(f => f.Id == id));
        }

        /// <summary>
        /// По умолчанию возвращает только активные пожары.
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        public IActionResult GetDeparturesList()
        {
            var deps = _dbContext.Departures
                    .Include(f => f.FireCars)
                    .Include(f => f.GpsPoint)
                    .Include(f => f.Operator)
                    .Include(f => f.History)
                    .Include(f => f.Images)
                    .OrderByDescending(f => f.DateTime);
                return Json(deps);
        }


        [HttpPost("")]
        public IActionResult AddDeparture([FromBody] Departure dep)
        {
            
            dep.History = new List<HistoryRecord>();
            dep.FireCars = new List<FireCar>();
            dep.Images = new List<Image>();
            dep.Operator = null;
            _dbContext.Departures.Add(dep);
            _dbContext.SaveChanges();
            return Json(dep);
        }

        [HttpPut("{id}")]
        public IActionResult EditFire(int id, [FromBody] Departure dep)
        {
            dep.Id = id;
            dep.Operator = _dbContext.Operators.FirstOrDefault(o => o.Id == dep.Operator.Id);
            //NOTE:Не работает редактирование списка вложений, истории и пожарных машины.
            _dbContext.Departures.Update(dep);
            _dbContext.SaveChanges();

            return Json(dep);
        }

        [HttpDelete("{id}")]
        public IActionResult DisableDeparture(int id)
        {
            var fire = _dbContext.Departures.FirstOrDefault(f => f.Id == id);
            if (fire != null)
            {
                fire.Active = false;
                _dbContext.SaveChanges();
            }
            return Ok();
        }

        [HttpPost("{idDep}/firecar")]
        public IActionResult AddFirecar(int idDep, [FromBody] FireCar car)
        {
            var fire = _dbContext.Departures
                            .Include(f => f.FireCars)
                            .Include(f => f.GpsPoint)
                            .Include(f => f.History)
                            .Include(f => f.Operator)
                            .Include(f => f.Images)
                            .FirstOrDefault(f => f.Id == idDep);
            if (fire != null)
            {
                var aCar = _dbContext.FireCars.FirstOrDefault(c => c.Id == car.Id);
                if (fire.FireCars == null)
                {
                    fire.FireCars = new List<FireCar>();
                }
                fire.FireCars.Add(aCar);
                _dbContext.SaveChanges();
            }
            return Json(fire);
        }

        [HttpDelete("{idDep}/firecar/{idCar}")]
        public IActionResult RemoveFireCar(int idDep, int idCar)
        {
            var fire = _dbContext.Departures
                    .Include(f => f.FireCars)
                    .Include(f => f.GpsPoint)
                    .Include(f => f.History)
                    .Include(f => f.Operator)
                    .Include(f => f.Images)
                    .FirstOrDefault(f => f.Id == idDep);
            if (fire != null)
            {
                if (fire.FireCars == null)
                {
                    fire.FireCars = new List<FireCar>();
                }
                fire.FireCars.Remove(_dbContext.FireCars.FirstOrDefault(fc => fc.Id == idCar));
                _dbContext.SaveChanges();
            }
            return Ok();
        }

        [HttpPost("{idDep}/image/")]
        public async Task<IActionResult> AddImage(int idDep, IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                Random rnd = new Random();

                var fileInfo = new FileInfo(uploadedFile.FileName);
                string path;
                do
                {
                    var randPath = new string(Enumerable.Repeat(rnd, 15).Select(x => (char)x.Next(97, 123)).ToArray());
                    path = "/Images/" + randPath + fileInfo.Extension;
                    fileInfo = new FileInfo(path);
                }
                while (fileInfo.Exists);

                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                var file = new Image() { Name = uploadedFile.FileName, Url = path };
                var departure = _dbContext.Departures.Include(f => f.Images).FirstOrDefault(f => f.Id == idDep);
                departure.Images.Add(file);
                _dbContext.SaveChanges();
                return Json(file);
            }
            return Json("Error");
        }

        [HttpDelete("{idDep}/image/{idImage}")]
        public IActionResult RemoveImage(int idDep, int idImage)
        {
            var departure = _dbContext.Departures.Include(f => f.Images).FirstOrDefault(f => f.Id == idDep);
            var image = departure.Images.FirstOrDefault(i => i.Id == idImage);
            if (image == null)
            {
                return Json("Ошибка!");
            }
            var path = image.Url;
            departure.Images.Remove(image);
            _dbContext.SaveChanges();
            var file = new FileInfo(_appEnvironment.WebRootPath + path);
            if (file.Exists)
            {
                System.IO.File.Delete(_appEnvironment.WebRootPath + path);
            }
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
