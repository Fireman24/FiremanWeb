// //Fireman->FiremanApi->DepartmentController.cs
// //andreygolubkow Андрей Голубков

using System.Linq;

using FiremanApi.DataBase;

using FiremanModel;


using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace FiremanApi.Api2
{
    [Produces("application/json")]
    [Route("api2/[controller]")]
    [EnableCors("CorsPolicy")]
    public class DepartmentController : Controller
    {
        private readonly FireContext _dbContext;

        public DepartmentController(FireContext context)
        {
            _dbContext = context;
        }

        [HttpGet("")]
        public IActionResult GetDepartments()
        {
            return Json(_dbContext.Departments.Include(d => d.GpsPoint).Include(d => d.FireCars));
        }

        [HttpGet("{id}")]
        public IActionResult GetDepartmentById(int id)
        {
            return Json(_dbContext.Departments.Include(d => d.GpsPoint).Include(d => d.FireCars).FirstOrDefault(d => d.Id == id));
        }

        [HttpPost("")]
        public IActionResult AddDepartment([FromBody] Department department)
        {
            _dbContext.Departments.Add(department);
            _dbContext.SaveChanges();
            return Json(department);
        }

        [HttpPut("{id}")]
        public IActionResult EditDepartment(int id,[FromBody] Department department)
        {
            department.Id = id;
            _dbContext.Departments.Update(department);
            _dbContext.SaveChanges();
            return Json(department);
        }

        [HttpDelete("{id}")]
        public IActionResult DisactivateDepartment(int id)
        {
            var department = _dbContext.Departments.FirstOrDefault(d => d.Id == id);
            if ( department != null )
            {
                department.Active = false;
            }
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
