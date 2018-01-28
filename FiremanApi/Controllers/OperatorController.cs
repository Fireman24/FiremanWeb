// //FireFight->FireFight->OperatorController.cs
// //andreygolubkow Андрей Голубков

using System;
using System.Collections.Generic;
using System.Linq;

using FiremanApi.Adapters;
using FiremanApi.DataBase;

using FiremanModel;

using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

namespace FiremanApi.Controllers
{
    [Route("[controller]/{operatorId}/")]
    [EnableCors("CorsPolicy")]
    public class OperatorController : Controller
    {

        private FireContext _dbContext;

        public OperatorController(FireContext context)
        {
            _dbContext = context;
        }

        
        /// <summary>
        /// Получает имя оператора по идентификатору.
        /// </summary>
        /// <param name="operatorId"></param>
        /// <returns></returns>
        [HttpGet("myname")]
        public IActionResult GetMyName(int operatorId)
        {
            OperatorZoneAndName data;
            try
            {
                var op = _dbContext.Operators.Include(o=>o.GeoZone).FirstOrDefault(o => o.Id == operatorId);
                data =new OperatorZoneAndName( op.Name, op.GeoZone); 
            }
            catch ( Exception )
            {
                try
                {
                    var op = _dbContext.Operators.Include(o => o.GeoZone).FirstOrDefault(o => o.Id == operatorId);
                    data = new OperatorZoneAndName(op.Name, op.GeoZone);
                }
                catch ( Exception )
                {
                    data = new OperatorZoneAndName("Нет связи с базой данных.", null);
                }
            }
            return Json(data);
        }

        [HttpGet("departments")]
        public IActionResult GetDepartmentsList(int operatorId)
        {
            return Json(_dbContext.Departments);
        }

        [HttpGet("departments/{departmentId}/cars")]
        public IActionResult GetDepartmentFirecarsList(int operatorId, int departmentId)
        {
            try
            { 
                _dbContext.Fires.Load();
                var firecarsList = _dbContext.Departments.Include(d => d.FireCars).FirstOrDefault(d => d.Id == departmentId).FireCars.Where(c=>c.Fire == null);
                foreach (FireCar fireCar in firecarsList)
                {
                    fireCar.Department = null;
                }
                return Json(firecarsList);
            }
            catch ( Exception exception )
            {
                return Json(exception);
            }
        }
                                                                                       
        [HttpPost("newtask")]
        public IActionResult NewTask(int operatorId,[FromBody] FireTask task)
        {
            try
            {
                if ( task.Address.Length < 2 )
                {
                    throw new Exception("Слишком короткий адрес");
                }   
                var fire = new Fire
                           {
                               Address = task.Address,
                               StartDateTime = DateTime.Now,
                               GpsPoint = new GpsPoint()
                                          {
                                              Lat = task.Lat,
                                              Lon = task.Lon
                                          }
                           };
                if ( task.IdDepartment != -1 )
                {
                    fire.Department = _dbContext.Departments.FirstOrDefault(d => d.Id == task.IdDepartment);
                }
                else
                {
                    throw new Exception("Укажите пожарную часть.");
                }
                if ( task.IdFirecar != -1 )
                {
                    fire.FireCars.Add(_dbContext.FireCars.FirstOrDefault(c => c.Id == task.IdFirecar));
                }
                fire.Operator = _dbContext.Operators.FirstOrDefault(o => o.Id == operatorId);
                _dbContext.Fires.Add(fire);
                _dbContext.SaveChanges();

                
                _dbContext.SaveChanges();
                return Json(new ResultResponse(200,"Заявка создана."));
            }
            catch ( Exception exception )
            {
                return Json(new ResultResponse(-1, exception.Message));
            }
        }

        [HttpGet("fire/{fireId}/data")]
        public IActionResult GetFireData(int operetorId, int fireId)
        {
            var fire = _dbContext.Fires.Include(f => f.Images)
                    .Include(f => f.Department)
                    .Include(f => f.GpsPoint)
                    .Include(f=>f.FireCars)
                    .FirstOrDefault(f => f.Id == fireId);
            foreach (var c in fire.FireCars)
            {
                c.Fire = null;
            }
            fire.Department.FireCars = null;
            return Json(fire);
        }

        [HttpPost("fire/{fireId}/update")]
        public IActionResult UpdateFire(int operatorId, int fireId,[FromBody] Fire fire)
        {
            bool isFinish = false;
            try
            {
                //var fire = JsonConvert.DeserializeObject<Fire>(o.ToString());
                var baseFire = _dbContext.Fires.Include(f => f.GpsPoint).FirstOrDefault(f => f.Id == fireId);
                baseFire.Address = fire.Address;
                baseFire.Comments = fire.Comments;
                
                baseFire.GpsPoint.Lat = fire.GpsPoint.Lat;
                baseFire.GpsPoint.Lon = fire.GpsPoint.Lon;
                baseFire.StartDateTime = fire.StartDateTime;
                if (baseFire.FinishDateTime != fire.FinishDateTime)
                {
                    baseFire.FinishDateTime = fire.FinishDateTime;
                    isFinish = true;
                }
                _dbContext.SaveChanges();


                return Json(new ResultResponse(200,"Заявка обновлена!"));
            }
            catch ( Exception exception )
            {
                return Json(new ResultResponse(-1, exception.Message));
            }
        }
            
        
        [HttpPut("fire/addcar")]
        public IActionResult AddFirecar(int operatorId,[FromBody] FirecarInFire data)
        {
            var fire = _dbContext.Fires.Include(f=>f.FireCars).FirstOrDefault(f => f.Id == data.IdFire);
            fire.FireCars.Add(_dbContext.FireCars.FirstOrDefault(f=>f.Id==data.IdFirecar));
            _dbContext.SaveChanges();
           
            _dbContext.SaveChanges(); 



            return Json(new ResultResponse(200,"Ок"));
        }
        
        [HttpPost("fire/removecar")]
        public IActionResult RemoveFirecar(int operatorId,[FromBody] FirecarInFire data)
        {
            var fire = _dbContext.Fires.Include(f=>f.FireCars).FirstOrDefault(f => f.Id == data.IdFire);
            fire.FireCars.Remove(_dbContext.FireCars.FirstOrDefault(f=>f.Id==data.IdFirecar));
            _dbContext.SaveChanges();
            
            return Json(new ResultResponse(200, "Ок"));
        }


        /// <summary>
        /// Показывает все заявки от оператора.
        /// </summary>
        /// <param name="operatorId">Идентификатор оператора.</param>
        /// <returns></returns>
        [HttpGet("fire/all")]
        public IActionResult GetAllFiresList(int operatorId)
        {
            var list = _dbContext.Fires.Where(f=>f.Operator.Id == operatorId).OrderByDescending(f=>f.StartDateTime);
            return Json(list);
        }

        [HttpGet("fire/active")]
        public IActionResult GetActiveFires(int operatorId)
        {
            
            var fireList = _dbContext.Fires.Include(f=>f.FireCars).Where(f => f.Operator.Id == operatorId).ToList();
            foreach (var fire in fireList)
            {
                foreach (var car in fire.FireCars)
                {
                    car.Fire = null;
                }
            }
            return Json(fireList);
        }

        [HttpGet("geoobjects")]
        public IActionResult GetGeoObjects(int operatorId)
        {
            //TODO: Добавить мониторинг машин
            var geoList = new List<GeoObject>();
            var fires = _dbContext.Fires.Include(o => o.GpsPoint).Where(f => f.FinishDateTime == null);
            foreach (Fire fire in fires)
            {
                geoList.Add(new GeoObject("fire",fire.GpsPoint,"Пожар:"+fire.Address,0));                
            }

            var departments = _dbContext.Departments.Include(o => o.GpsPoint);
            foreach (var department in departments)
            {
                geoList.Add(new GeoObject("department", department.GpsPoint,"Пожарная часть: "+department.Name,0));
            }

            var hydrants = _dbContext.Hydrants.Where(h => h.Active).Include(o=>o.GpsPoint);
            foreach (var hydrant in hydrants)
            {
                if ( hydrant.Active == false )
                {
                    continue;
                }
                geoList.Add(new GeoObject("hydrant",hydrant.GpsPoint,"Пожарный гидрант. Дата проверки: "+hydrant.RevisionDate.Date,0));    
            }

            var cars = _dbContext.FireCars.Include(o => o.GpsPoint);
            foreach (var car in cars)
            {
                if (car.GpsPoint == null)
                {
                    continue;
                }
                geoList.Add(new GeoObject("firecar", car.GpsPoint, "Пожарная машина " + car.Name,car.Id));
            }

            return Json(geoList);
        }

       

    }
}
