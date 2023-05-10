using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TowerDefenseAPI.Data;
using TowerDefenseAPI.Domain.Models;

namespace TowerDefenseAPI.Controllers
{
    public class JwtController : Controller
    {
        ApplicationDbContext context;
        public JwtController(ApplicationDbContext _context) => context = _context;

        [HttpPost("SignIn")]
        public IActionResult Token(string login, string password)
        {
            var identity = GetIdentity(login, password);
            if (identity == null)
                return BadRequest(new { errorText = "Неправильный логин или пароль" });

            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: DateTime.UtcNow,
                claims: identity.Claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                Token = encodedJwt,
                Login    = identity.Name
            };

            return Ok(response);
        }

        private ClaimsIdentity? GetIdentity(string login, string password)
        {
            User user = context.Users.FirstOrDefault(x => x.Login == login);

            if (user != null)
            {
                bool isValidPassword = BCrypt.Net.BCrypt.Verify(password, user.Password);

                if (isValidPassword)
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
                };

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token",
                        ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);

                    return claimsIdentity;
                }
                return null;
            }
            return null;
        }
    }
}
