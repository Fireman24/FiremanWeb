using Microsoft.AspNetCore.Mvc;
using FiremanApi.DataBase;

using FiremanModel;

using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace FiremanApi.Controllers
{
    [Produces("application/json")]
    [Route("api")]
    public class ApiController : Controller
    {
        private readonly FireContext _dbContext;
        private readonly IHostingEnvironment _appEnvironment;

        public ApiController(FireContext context, IHostingEnvironment appEnvironment)
        {
            _dbContext = context;
            _appEnvironment = appEnvironment;
        }

        /// <summary>
        /// Вернет список пожарных частей.
        /// </summary>
        /// <returns>Список ПЧ.</returns>
        [HttpGet("department")]
        public IActionResult GetDepartments()
        {
            var list = _dbContext.Departments.Include(d => d.GpsPoint).Include(d => d.FireCars);
            foreach (var department in list)
            {
                foreach (FireCar car in department.FireCars)
                {
                    car.Department = null;                    
                }
            }
            return Json(list);
        }

        /// <summary>
        /// Вернет список гидрантов.
        /// </summary>
        /// <returns>Список гидрантов.</returns>
        [HttpGet("hydrant")]
        public IActionResult GetHydrants()
        {
            var list = _dbContext.Hydrants.Include(h => h.GpsPoint);
            return Json(list);
        }

        /// <summary>
        /// Вернет список операторов.
        /// </summary>
        /// <returns>Список операторов.</returns>
        [HttpGet("operator")]
        public IActionResult GetOperators()
        {
            var list = _dbContext.Operators.Include(o => o.Fires).Include(o => o.GeoZone);
            foreach (var op in list)
            {
                foreach (var fire in op.Fires)
                {
                    fire.Operator = null;
                }
            }
            return Json(list);
        }



    }
}
