using System;
using System.Linq;

using FiremanApi.DataBase;

using FiremanModel;

using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FiremanApi.Controllers
{

    //NOTE: При получении списка, загружать только общие данные
    // При получении конкретного объекта загружать полную информацию

    /// <summary>
    /// Позволяет редактировать общую базу данных.
    /// Пожарные гидранты, добавление машин итд.
    /// Не занимается пожарами.
    /// </summary>
    [Route("[controller]")]
    [EnableCors("CorsPolicy")]
    public class SettingsController : Controller
    {
        private FireContext _dbContext;
                                                         
        public SettingsController(FireContext context)
        {
            _dbContext = context;
            
        }

        /// <summary>
        /// Показываем всех операторов.
        /// </summary>
        /// <returns>Список операторов.</returns> 
        [HttpGet("operators")]
        public IActionResult GetOperatorsList()
        {
            try
            {
                var operators = _dbContext.Operators.ToList();
                Response.StatusCode = 200;
                return Json(operators);
            }
            catch ( Exception exception )
            {
                Response.StatusCode = 404;
                return Json(exception.Message);
            }
        }

        /// <summary>
        /// Получает одного оператора.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns>Данные оператора.</returns>
        [HttpGet("operators/{id}")]
        public IActionResult GetOperator(int id)
        {
            var o = _dbContext.Operators.Include(op=>op.GeoZone).FirstOrDefault(op=>op.Id == id);
            if ( o == null )
            {
                return Json("Нет оператора с таким id.");
            }
            return Json(o);
        }



        /// <summary>
        /// Редактирует данные оператора.
        /// </summary>
        /// <param name="id">Идентификатор оператора.</param>
        /// <param name="oIn">Данные оператора.</param>
        /// <returns>Результат операции.</returns>
        [HttpPost("operators/{id}/edit")]
        public IActionResult EditOperator(int id, [FromBody] Operator oIn)
        {
            var op = _dbContext.Operators.Include(opr=>opr.Fires).Include(opr=>opr.GeoZone).FirstOrDefault(opr => opr.Id == id);
            if ( op == null )
            {
                return Json("Нет оператора с таким id.");
            }
            try
            {
                op.Key = oIn.Key;
                op.Name = oIn.Name;
                op.GeoZone.Lat = oIn.GeoZone.Lat;
                op.GeoZone.Lon = oIn.GeoZone.Lon;
                _dbContext.SaveChanges();
                return Json("Изменено");
            }
            catch ( Exception exception )
            {
                return Json(exception.Message);
            }
        }

        /// <summary>
        /// Добавляет оператора.
        /// </summary>
        /// <param name="o">Оператор.</param>
        /// <returns>Результат добавления.</returns>
        [HttpPost("operators")]
        public IActionResult AddOperator([FromBody] Operator o)
        {
            try
            {
                _dbContext.Operators.Add(o);
                _dbContext.SaveChanges();
                return Json("Добавлено");
            }
            catch ( Exception exception )
            {
                return Json(exception.Message);
            }
        }

        /// <summary>
        /// Удаляет оператора.
        /// </summary>
        /// <param name="id">Идентификатор оператора для удаления.</param>
        /// <returns>Результат удаления оператора.</returns>
        [HttpDelete("operators/{id}/remove")]
        public IActionResult DeleteOperator(int id)
        {
            try
            {
                _dbContext.Operators.Remove(_dbContext.Operators.FirstOrDefault(o => o.Id == id));
                _dbContext.SaveChanges();
                return Json("Удалено.");
            }
            catch ( Exception exception )
            {
                Response.StatusCode = 200;
                return Json(exception.Message);
            }  
        }


        /// <summary>
        /// Получает все пожарные части.
        /// </summary>
        /// <returns>Список пожарных частей.</returns>
        [HttpGet("departments")]
        public IActionResult GetDepartmentsList()
        {
            var departments = _dbContext.Departments;
            return Json(departments);
        }

        /// <summary>
        /// Получает конкретный отдел по id.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns>Данные отдела.</returns>
        [HttpGet("departments/{id}")]
        public IActionResult GetDepartment(int id)
        {
            var dep = _dbContext.Departments.Include(d => d.GpsPoint).Include(d => d.FireCars).FirstOrDefault(d => d.Id == id);
            foreach (var c in dep.FireCars)
            {
                c.Department = null;
            }
            return Json(dep);
        }

        /// <summary>
        /// Редактирует пожарную часть(отдел).
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="department">Данные для новой части.</param>
        /// <returns>Результат операции.</returns>
        [HttpPost("departments/{id}/edit")]
        public IActionResult EditDepartment(int id, [FromBody] Department department)
        {
            try
            {
                var dep = _dbContext.Departments.Include(d=>d.GpsPoint).FirstOrDefault(d => d.Id == id);
                dep.GpsPoint.Lat = department.GpsPoint.Lat;
                dep.GpsPoint.Lon = department.GpsPoint.Lon;
                dep.Address = department.Address;
                dep.Name = department.Name;
                _dbContext.SaveChanges();
                return Json(department);
            }
            catch ( Exception exception )
            {
                return Json(exception.Message);
            }
        }

        /// <summary>
        /// Добавляет пожарную часть.
        /// </summary>
        /// <param name="department">Пожарная часть.</param>
        /// <returns>Результат добавления части.</returns>
        [HttpPost("departments")]
        public IActionResult AddDepartment([FromBody] Department department)
        {
            try
            {
                _dbContext.Departments.Add(department);
                _dbContext.SaveChanges();
                return Json(department);
            }
            catch ( Exception exception )
            {
                return Json(exception.Message);
            }
        }

        /// <summary>
        /// Удаляет пожарную часть из бд.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns>Результат операции.</returns>
        [HttpDelete("departments/{id}/remove")]
        public IActionResult DeleteDepartment(int id)
        {
            try
            {
                var dep = _dbContext.Departments.Include(d=>d.FireCars).Include(d=>d.GpsPoint).FirstOrDefault(d => d.Id == id);
                foreach (var car in dep.FireCars)
                {
                    _dbContext.FireCars.Remove(car);    
                }
                _dbContext.SaveChanges();
                _dbContext.Departments.Remove(dep);
                _dbContext.SaveChanges();
                return Json("Удалено.");
            }
            catch ( Exception exception )
            {
                return Json(exception.Message);
            }
        }

        /// <summary>
        /// Получает все пожарные машинки.
        /// </summary>
        /// <returns>Пожарные машины.</returns>
        [HttpGet("firecars")]
        public IActionResult GetFireCarsList()
        {
            _dbContext.Departments.Load();
            var cars = _dbContext.FireCars.Include(c => c.GpsPoint);
            foreach (var c in  cars)
            {
                c.Department.FireCars = null;
            }
            return Json(cars);
        }

        /// <summary>
        /// Получает данные пожарной машины по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns>Данные пожарной машины.</returns>
        [HttpGet("firecars/{id}")]
        public IActionResult GetFireCar(int id)
        {
            var firecar = _dbContext.FireCars.Include(c => c.GpsPoint).Include(c => c.Department).Include(c => c.Fire).FirstOrDefault(c=>c.Id==id);
            try
            {
                firecar.Department.FireCars = null;
                firecar.Fire.Department = null;
                firecar.Fire.FireCars = null;
            }
            catch ( Exception )
            {
                // ignored
            }
            return Json(firecar);
        }

        /// <summary>
        /// Редактирует пожарную машину.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="fireCar">Новые данные пожарной машины.</param>
        /// <returns>Результат орперации.</returns>
        [HttpPost("firecars/{id}/edit")]
        public IActionResult EditFireCar(int id, [FromBody] FireCar fireCar)
        {
            try
            {
                var car = _dbContext.FireCars.Include(c=>c.Department).FirstOrDefault(c=>c.Id == id);
                car.Name = fireCar.Name;
                car.Num = fireCar.Num;
                car.Department = _dbContext.Departments.FirstOrDefault(d => d.Id == fireCar.Department.Id);
                _dbContext.SaveChanges();
                car.Department.FireCars = null;
                return Json("Изменено");
            }
            catch ( Exception exception )
            { 
                return Json(exception.Message);
            }
        }

        /// <summary>
        /// Добавляет пожарную машину.
        /// </summary>
        /// <param name="car">Данные пожарной машины.</param>
        /// <returns>Результат операции.</returns>
        [HttpPost("firecars")]
        public IActionResult AddFireCar([FromBody] FireCar car)
        {
            try
            {
                var gps = new GpsPoint();
                _dbContext.GpsPoints.Add(gps);
                _dbContext.SaveChanges();
                var newCar = new FireCar
                             {
                                 Name = car.Name,
                                 Num = car.Num,
                                 Department = _dbContext.Departments.FirstOrDefault(d => d.Id == car.Department.Id),
                                 GpsPoint = gps
                             };

                _dbContext.FireCars.Add(newCar);
                _dbContext.SaveChanges();
                newCar.Department.FireCars = null;
                return Json("Добавлено");
            }
            catch ( Exception exception )
            {
                return Json(exception.Message);
            }      
        }

        /// <summary>
        /// Удаляем пожарную машину.
        /// </summary>
        /// <param name="id">Идентификатор машины.</param>
        /// <returns>Результат операции.</returns>
        [HttpDelete("firecars/{id}/remove")]
        public IActionResult DeleteFireCar(int id)
        {
            try
            {
                _dbContext.FireCars.Remove(_dbContext.FireCars.FirstOrDefault(c => c.Id == id));
                _dbContext.SaveChanges();
                return Json("Удалено");
            }
            catch ( Exception exception )
            {
                return Json(exception.Message);
            }
        }

        /// <summary>
        /// Получает список пожарных гидрантов.
        /// </summary>
        /// <returns>Список пожарных гидрантов.</returns>
        [HttpGet("hydrants")]
        public IActionResult GetHydrantsList()
        {
            var hydrants = _dbContext.Hydrants.Include(h => h.GpsPoint);
            return Json(hydrants);
        }

        /// <summary>
        /// Добавляет гидрант.
        /// </summary>
        /// <param name="hydrant">Данные гидранта.</param>
        /// <returns>Результат операции.</returns>
        [HttpPost("hydrants")]
        public IActionResult AddHydrant([FromBody] Hydrant hydrant)
        {
            try
            {
                var h = new Hydrant();
                h.Active = hydrant.Active;
                h.Responsible = hydrant.Responsible;
                h.RevisionDate = hydrant.RevisionDate;
                var gps = new GpsPoint();
                gps.Lat = hydrant.GpsPoint.Lat;
                gps.Lon = hydrant.GpsPoint.Lon;
                _dbContext.Add(gps);
                h.GpsPoint = gps;
                _dbContext.Hydrants.Add(h);
                _dbContext.SaveChanges();
                return Json(h);
            }
            catch ( Exception exception )
            {
                return Json(exception.Message);
            }
        }

        /// <summary>
        /// Получает данные пожарного гидранта по id.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns>Данные гидранта.</returns>
        [HttpGet("hydrants/{id}")]
        public IActionResult GetHydrant(int id)
        {
            var hydrant = _dbContext.Hydrants.Include(h=>h.GpsPoint).FirstOrDefault(h => h.Id == id);
            return Json(hydrant);
        }

        /// <summary>
        /// Редактирует гидрант. 
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="hydrant">Данные гидранта.</param>
        /// <returns>Результат операции.</returns>
        [HttpPost("hydrants/{id}/edit")]
        public IActionResult EditHydrant(int id, [FromBody] Hydrant hydrant)
        {
            try
            {
                var h = _dbContext.Hydrants.Include(hyd=>hyd.GpsPoint).FirstOrDefault(hd => hd.Id == id);
                h.GpsPoint.Lat = hydrant.GpsPoint.Lat;
                h.GpsPoint.Lon = hydrant.GpsPoint.Lon;
                h.Active = hydrant.Active;
                h.Responsible = hydrant.Responsible;
                h.RevisionDate = hydrant.RevisionDate;
                _dbContext.SaveChanges();
                return Json(h);
            }
            catch ( Exception exception )
            {
                return Json(exception.Message);

            }
        }

        /// <summary>
        /// Удаляем гидрант.
        /// </summary>
        /// <param name="id">Идентификатор гидранта.</param>
        /// <returns>Результат операции.</returns>
        [HttpDelete("hydrants/{id}/remove")]
        public IActionResult DeleteHydrant(int id)
        {
            try
            {
                _dbContext.Hydrants.Remove(_dbContext.Hydrants.FirstOrDefault(h => h.Id == id));
                _dbContext.SaveChanges();
                return Json("Удалено.");
            }
            catch ( Exception exception )
            {
                return Json(exception.Message);
            }
        }

        public IActionResult Index()
        {
            return Ok();
        }

        
    }
}
