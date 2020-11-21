using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JWTTest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using JWTTest.Services;

namespace JWTtest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config; // To read from the config file
        private IJWTService _jwtService;

        private List<User> appUsers = new List<User>
        {
        new User { FullName = "Vaibhav Bhapkar", UserName = "admin", Password = "1234", UserRole = "Admin" },
        new User { FullName = "Test User", UserName = "user", Password = "1234", UserRole = "User" }
        };

        public LoginController(IConfiguration config, IJWTService jWTService)
        {
            _config = config;
            _jwtService = jWTService;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] User login)
        {
            if (login == null)
                return BadRequest(new { error = "Validate Input"});
            
            User user = AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = _jwtService.GenerateJWTtoken(user);

                return Ok(new
                {
                    token = tokenString
                });
            }

            return Unauthorized();
        }

        private User AuthenticateUser(User loginCredentials)
        {
            User user = appUsers.SingleOrDefault(x => x.UserName == loginCredentials.UserName && x.Password == loginCredentials.Password);
            return user;
        }
    }
}