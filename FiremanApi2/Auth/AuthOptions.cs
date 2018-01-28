// //Fireman->FiremanApi->AuthOptions.cs
// //andreygolubkow Андрей Голубков

using System.Text;

using Microsoft.IdentityModel.Tokens;

namespace FiremanApi2.Auth
{
    public class AuthOptions
    {
        public const string ISSUER = "Fire24"; 
        public const string AUDIENCE = "http://localhost:5000/"; // потребитель токена
        const string KEY = "fire!fire24!fire";   // ключ для шифрации
        public const int LIFETIME = 5; // время жизни токена в минутах
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
