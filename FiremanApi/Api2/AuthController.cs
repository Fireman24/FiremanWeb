// //Fireman->FiremanApi->AuthController.cs
// //andreygolubkow Андрей Голубков

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FiremanApi.Api2
{
    [Route("api2/[controller]")]
    public class AuthController : Controller
    {
        private readonly IHttpContextAccessor _context;

        public AuthController(IHttpContextAccessor context)
        {
            _context = context;
        }

        [HttpGet("token")]
        public dynamic GetToken()
        {
            var handler = new JwtSecurityTokenHandler();

            var sec = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(sec));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var identity = new ClaimsIdentity(new GenericIdentity("temp@jwt.ru"), new[] { new Claim("user_id", "57dc51a3389b30fed1b13f91") });
            var token = handler.CreateJwtSecurityToken(subject: identity,
                                                       signingCredentials: signingCredentials,
                                                       audience: "ExampleAudience",
                                                       issuer: "ExampleIssuer",
                                                       expires: DateTime.UtcNow.AddSeconds(42));
            return handler.WriteToken(token);
        }


        [Authorize, HttpGet("secure")]
        public dynamic Secret()
        {
            var currentUser = _context.HttpContext.User;
            return currentUser.Identity.Name;
        }

    }
}
