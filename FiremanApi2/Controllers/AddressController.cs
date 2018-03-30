using System.Linq;
using System.Security.Claims;

using FiremanApi2.DataBase;
using FiremanApi2.Model;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FiremanApi2.Controllers
{
    
    /// <summary>
    /// Позволяет работать с адресами(объектами).
    /// </summary>
    [Produces("application/json")]
    [Route("api2/[controller]")]
    [EnableCors("CorsPolicy")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AddressController : Controller
    {
        /// <summary>
        /// Контекст БД.
        /// </summary>
        private readonly FireContext _dbContext;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        public AddressController(FireContext context)
        {
            _dbContext = context;
        }
 
        /// <summary>
        /// Получает объект по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns>Адрес.</returns>
        [HttpGet("{id}")]
        public IActionResult GetAddress(int id)
        {
            var address = _dbContext.Addresses.Include(a => a.Department).Include(a=>a.GpsPoint).FirstOrDefault(a => a.Id == id);
            return Json(address);
        }

        /// <summary>
        /// Удаляет адрес их базы данных по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns>Ок, если удаление успешно.</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteAddress(int id)
        {
            var address = _dbContext.Addresses.FirstOrDefault(a => a.Id == id);
            if ( address == null )
            {
                return NotFound();
            }
            _dbContext.Addresses.Remove(address);
            _dbContext.SaveChanges();
            return Ok();
        }


        /// <summary>
        /// Получает адрес по его началу. Регистр не учитывается.
        /// </summary>
        /// <param name="value">Начало адреса, если пусто - то вернёт все.</param>
        /// <returns>Список адресов.</returns>
        [HttpGet("")]
        public IActionResult GetAddressStartsWith([FromQuery] string value="")
        {
            var addresses = _dbContext.Addresses.Include(a=>a.Department).Include(a=>a.GpsPoint).Where(a => a.Label.ToLower().StartsWith(value.ToLower()));
            return Json(addresses);
        }

        /// <summary>
        /// Добавляет адрес в базу данных. 
        /// </summary>
        /// <param name="address">Адрес, который требуется добавить в БД.</param>
        /// <returns>Возвращает объект, добавленный в БД.</returns>
        [HttpPost("")]
        public IActionResult AddAddress([FromBody] Address address)
        {
            if ( address.Department != null )
            {
                address.Department = _dbContext.Departments.FirstOrDefault(d => d.Id == address.Department.Id);
            }
            _dbContext.Addresses.Add(address);
            _dbContext.SaveChanges();
            return Json(address);
        }
        
        /// <summary>
        /// Редактирует адрес.
        /// </summary>
        /// <param name="id">Идентификатор адреса, для редактирования.</param>
        /// <param name="address">Редактируемый объект.</param>
        /// <returns>Обновлённый адрес из БД.</returns>
        [HttpPut("{id}")]
        public IActionResult EditAddress(int id,[FromBody] Address address)
        {
            address.Id = id;
            if ( address.GpsPoint != null )
            {
                _dbContext.GpsPoints.Update(address.GpsPoint);
            }
            _dbContext.Addresses.Update(address);
            _dbContext.SaveChanges();
            return Json(address);
        }
    }
}