// //firemanapi->FiremanApi->LoginController.cs
// //andreygolubkow Андрей Голубков

using FiremanApi.DataBase;

using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FiremanApi.Controllers
{
    /// <summary>
    /// Занимается работой с данными для входа.
    /// </summary>
    
    [Route("[controller]")]
    [EnableCors("CorsPolicy")]
    public class LoginController : Controller
    {
        private FireContext _dbContext;
                                                         
        public LoginController(FireContext context)
        {
            _dbContext = context;
        }

        [HttpGet("operators")]
        public IActionResult GetOperatorsList()
        {
            return Json(_dbContext.Operators.Include(oper=>oper.GeoZone));
        }
    }
}
