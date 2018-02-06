// //Fireman->FiremanApi->Operator.cs
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
    public class OperatorController : Controller
    {
        private FireContext _dbContext;

        public OperatorController(FireContext context)
        {
            _dbContext = context;
        }

        [HttpGet("")]
        public IActionResult GetOperators()
        {
            return Json(_dbContext.Operators);
        }

        [HttpPost("")]
        public IActionResult AddOperator([FromBody] Operator op)
        {
            _dbContext.Operators.Add(op);
            _dbContext.SaveChanges();
            return Json(op);
        }

        /// <summary>
        /// Деактивирует оператора.
        /// Удалять оператора нельзя, т.к. могут быть записи в БД, ссылающиеся на него.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns>Оператор.</returns>
        [HttpDelete("{id}")]
        public IActionResult DisactivateOperator(int id)
        {
            var op =_dbContext.Operators.FirstOrDefault(o => o.Id == id);
            op.Active = false;
            _dbContext.SaveChanges();
            return Ok();
        }


        [HttpGet("{id}")]
        public IActionResult GetOperatorById(int id)
        {
            return Json(_dbContext.Operators.Include(o=>o.GeoZone).Include(o=>o.Fires).FirstOrDefault(o=>o.Id==id));
        }

        
        [HttpPut("{id}")]
        public IActionResult EditOperator(int id,[FromBody] Operator o)
        {
            o.Id = id;
            if (o.GeoZone != null)
            {
                _dbContext.GpsPoints.Update(o.GeoZone);
            }
            _dbContext.Operators.Update(o);
            _dbContext.SaveChanges();
            return Json(o);
        }
    }
}
