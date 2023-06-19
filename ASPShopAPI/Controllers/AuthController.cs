using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ASPShopAPI.Data;
using ASPShopAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;

namespace ASPShopAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ShopDbContext _context;

        public AuthController(IConfiguration config, ShopDbContext context)
        {
            _config = config;
            _context = context;
        }

        [HttpPost("generate-jwt")]
        public JsonResult GenerateToken(Login login)
        {
            // find user by email
            var user = _context.Users.FirstOrDefault(u => u.Email == login.Email);

            // check if user exists and password matches
            if (user == null || !VerifyPassword(login.Password, user.Password))
            {
                return new JsonResult(new {
                    error = "Invalid credentials" }) { StatusCode = 401 };

            }

            // generate token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("SECRET_KEY"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.Email, login.Email)
                }),
                Expires = DateTime.UtcNow.AddMinutes(
                    int.Parse("60")),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return  new JsonResult(
                Ok(new { Token = tokenHandler.WriteToken(token) }));
        }

        private bool VerifyPassword(string password, string passwordHash)
        {
            byte[] passwordHashBytes = Encoding.UTF8.GetBytes(passwordHash);
            string hashedPassword = Encoding.UTF8.GetString(passwordHashBytes);
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}

