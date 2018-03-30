// //Fireman->FiremanApi2->TokenController.cs
// //andreygolubkow Андрей Голубков

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

using FiremanApi2.DataBase;
using FiremanApi2.Model;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FiremanApi2.Auth
{
    [Route("api2/[controller]")]
    public class TokenController : Controller
    {
        private IConfiguration _config;
        private FireContext _dbContext;

        public TokenController(IConfiguration config, FireContext context)
        {
            _config = config;
            _dbContext = context;
        }

        [HttpGet("check")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult CheckToken()
        {
            var userClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Properties.Values.Contains(JwtRegisteredClaimNames.NameId));
            var user = _dbContext.Operators.Include(o=>o.GeoZone).FirstOrDefault(u => u.Id == Convert.ToInt32(userClaim.Value));
            user.Key = "null";
            return Json(user);
        }
        
        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateToken([FromBody]LoginModel login)
        {
            IActionResult response = Unauthorized();
            var user = Authenticate(login);

            if (user != null)
            {
                var tokenString = BuildToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        private string BuildToken(Operator user)
        {

            var claims = new[] {
                                       new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                                       new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                                       new Claim(JwtRegisteredClaimNames.UniqueName, user.Login),
                                       new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                               };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                                             _config["Jwt:Issuer"],
                                             claims,
                                             expires: DateTime.Now.AddHours(24),
                                             signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private Operator Authenticate(LoginModel login)
        {
            //Если вообще никого нет в БД, то делаем нового юзера
            if ( !_dbContext.Operators.Any() )
            {
                _dbContext.Operators.Add(new Operator()
                                         {
                                                 Active = true,
                                                 Login = "admin",
                                                 Name = "Admin",
                                                 Role = "Admin",
                                                 Key = "fireman",
                                                 GeoZone = new GpsPoint()
                                         });
                _dbContext.SaveChanges();
            }
            
            Operator user = _dbContext.Operators.FirstOrDefault(o=>o.Login == login.Username && o.Key == login.Password && o.Active);

            return user;
        }


    }
}
