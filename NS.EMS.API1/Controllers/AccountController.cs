using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NS.EMS.API1.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace NS.EMS.API1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Login(Login login)
        {
            using(var contect = new EmployeeDBContext())
            {
                var user = contect.Login.Where(x => x.UserName == login.UserName && x.Password==login.Password && x.IsActive==true).FirstOrDefault();
                if(user!=null)
                {
                    var token = GenerateToken(user.UserName, user.Role);
                    return Ok(token);
                }
                else
                {
                    return Ok("Invalid Username or Password.");
                }
            }
            return Ok();
        }
        private string GenerateToken(string username,string role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new Claim[] { new Claim (ClaimTypes.Name, username), new Claim (ClaimTypes.Role, role) };
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires:System.DateTime.Now.AddMinutes(60),
                signingCredentials:credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
