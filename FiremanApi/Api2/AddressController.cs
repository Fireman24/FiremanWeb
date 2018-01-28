using System.Linq;

using FiremanApi.DataBase;

using FiremanModel;
using Microsoft.EntityFrameworkCore;

namespace FiremanApi.Api2
{
    [Produces("application/json")]
    [Route("api2/[controller]")]
    [EnableCors("CorsPolicy")]
    public class AddressController : Controller
    {
        private readonly FireContext _dbContext;

        public AddressController(FireContext context)
        {
            _dbContext = context;
        }

        [HttpGet("{id}")]
        public IActionResult GetAddress(int id)
        {
            var address = _dbContext.Addresses.Include(a => a.Department).FirstOrDefault(a => a.Id == id);
            return Json(address);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAddress(int id)
        {
            _dbContext.Addresses.Remove(_dbContext.Addresses.FirstOrDefault(a => a.Id == id));
            _dbContext.SaveChanges();
            return Ok();
        }


        [HttpGet("")]
        public IActionResult GetAddressStartsWith([FromQuery] string value="")
        {
            var addresses = _dbContext.Addresses.Include(a=>a.Department).Where(a => a.Label.ToLower().StartsWith(value.ToLower()));
            return Json(addresses);
        }

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

        

    }

}