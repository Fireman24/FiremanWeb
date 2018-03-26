using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using FiremanApi2.DataBase;
using FiremanApi2.Model;

using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FiremanApi2.Controllers
{

    [Route("api2/[controller]")]
    [EnableCors("CorsPolicy")]
    public class FireController : Controller
    {
        private readonly FireContext _dbContext;
        private readonly IHostingEnvironment _appEnvironment;


        public FireController(FireContext context, IHostingEnvironment appEnvironment)
        {
            _dbContext = context;
            _appEnvironment = appEnvironment;
        }

        [HttpGet("{id}")]
        public IActionResult GetFire(int id)
        {
            var fires = _dbContext.Fires.Include(f => f.Department)
                            .Include(f => f.FireCars)
                            .Include(f => f.GpsPoint)
                            .Include(f => f.Operator)
                            .Include(f=>f.History)
                            .Include(f => f.Images);
            return Json(fires.FirstOrDefault(f => f.Id == id));
        }

        /// <summary>
        /// �� ��������� ���������� ������ �������� ������.
        /// </summary>
        /// <param name="all"></param>
        /// <returns></returns>
        [HttpGet("")]
        public IActionResult GetFiresList([FromQuery] bool all=false)
        {
            var fires = _dbContext.Fires.Include(f => f.Department)
                    .Include(f => f.FireCars)
                    .Include(f => f.GpsPoint)
                    .Include(f => f.Operator)
                    .Include(f => f.History)
                    .Include(f => f.Images)
                    .OrderByDescending(f => f.StartDateTime);
            if ( all )
            {
                return Json(fires);
            }
            return Json(fires.Where(f => f.FinishDateTime == null).OrderByDescending(f => f.StartDateTime));
        }


        [HttpPost("")]
        public IActionResult AddFire([FromBody] Fire fire)
        {
            fire.Department = _dbContext.Departments.FirstOrDefault(d => d.Id == fire.Department.Id);
            fire.History = new List<HistoryRecord>();
            fire.FireCars = new List<FireCar>();
            fire.Images = new List<Image>();
            //fire.Operator = _dbContext.Operators.FirstOrDefault(o => fire.Operator.Id == o.Id);
            //TODO: ������� ����������� ��������� �� ������
            fire.Operator = null;
            if (fire.GpsPoint != null)
            {
                _dbContext.GpsPoints.Add(fire.GpsPoint);
            }
            _dbContext.Fires.Add(fire);
            _dbContext.SaveChanges();
            return Json(fire);
        }

        [EnableCors("CorsPolicy")]
        [HttpPut("{id}")]
        public IActionResult EditFire(int id,[FromBody] Fire fire)
        {
            fire.Id = id;
            if ( fire.Department != null )
            {
                fire.Department = _dbContext.Departments.AsNoTracking().FirstOrDefault(d => d.Id == fire.Department.Id);
            }
            if ( fire.Operator != null )
            {
                fire.Operator = _dbContext.Operators.AsNoTracking().FirstOrDefault(o => o.Id == fire.Operator.Id);
            }
            if (fire.GpsPoint != null)
            {
                _dbContext.GpsPoints.Update(fire.GpsPoint);
            }

            //NOTE:�� �������� �������������� ������ ��������, ������� � �������� ������.
            _dbContext.Fires.Update(fire);
            _dbContext.SaveChanges();

            return Json(fire);
        }

        [HttpDelete("{id}")]
        public IActionResult DisableFire(int id)
        {
            var fire = _dbContext.Fires.FirstOrDefault(f => f.Id == id);
            if ( fire != null )
            {
                fire.Active = false;
                _dbContext.SaveChanges();
            }
            return Ok();
        }

        [HttpPost("{idFire}/firecar")]
        public IActionResult AddFirecar(int idFire, [FromBody] FireCar car)
        {
            var fire = _dbContext.Fires
                            .Include(f=>f.FireCars)
                            .Include(f=>f.Department)
                            .Include(f=>f.GpsPoint)
                            .Include(f=>f.History)
                            .Include(f=>f.Operator)
                            .Include(f=>f.Images)
                            .FirstOrDefault(f => f.Id == idFire);
            if ( fire != null )
            {
                var aCar = _dbContext.FireCars.FirstOrDefault(c=>c.Id==car.Id);
                if ( fire.FireCars == null )
                {
                    fire.FireCars = new List<FireCar>();
                }
                fire.FireCars.Add(aCar);
                _dbContext.SaveChanges();
            }
            return Json(fire);
        }

        [HttpDelete("{idFire}/firecar/{idCar}")]
        public IActionResult RemoveFireCar(int idFire, int idCar)
        {
            var fire = _dbContext.Fires
                    .Include(f => f.FireCars)
                    .Include(f => f.Department)
                    .Include(f => f.GpsPoint)
                    .Include(f => f.History)
                    .Include(f => f.Operator)
                    .Include(f => f.Images)
                    .FirstOrDefault(f => f.Id == idFire);
            if ( fire!=null )
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

        [HttpPost("{idFire}/image/")]
        public async Task<IActionResult> AddImage(int idFire, IFormFile uploadedFile)
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
                var fire = _dbContext.Fires.Include(f => f.Images).FirstOrDefault(f => f.Id == idFire);
                fire.Images.Add(file);
                _dbContext.SaveChanges();
                return Json(file);
            }
            return Json("Error");
        }

        [HttpDelete("{idFire}/image/{idImage}")]
        public IActionResult RemoveImage(int idFire,int idImage)
        {
            var fire = _dbContext.Fires.Include(f=>f.Images).FirstOrDefault(f => f.Id == idFire);
            var image = fire.Images.FirstOrDefault(i => i.Id == idImage);
            if (image == null)
            {
                return Json("������!");
            }
            var path = image.Url;
            fire.Images.Remove(image);
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