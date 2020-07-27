using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnlineShopping.Buisness.DTOs;
using OnlineShopping.DataAccess.Models;
using OnlineShopping.DataAccess.Repository;
using OnlineShopping.DataAccess.Repository.Interfaces;

namespace OnlineShopping.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository<Customers> repo;
        private readonly IConfiguration config;

        public AuthController(IAuthRepository<Customers> repo, IConfiguration config)
        {
            this.repo = repo;
            this.config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            userForRegisterDto.Email = userForRegisterDto.Email.ToLower();

            if (await repo.UserExists(userForRegisterDto.Email))
                return BadRequest("User exists");

            var userToCreate = new Customers
            {
                Email = userForRegisterDto.Email,
                City = userForRegisterDto.City,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                Phone = userForRegisterDto.Phone,
                State = userForRegisterDto.State,
                Street = userForRegisterDto.street,
                ZipCode = userForRegisterDto.ZipCode

            };

            var createdUser = await repo.Register(userToCreate, userForRegisterDto.Password);

            //return StatusCode(201);
            return Ok(createdUser);

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {

            //var userFromRepo = await repo.Login(userForLoginDto.Email, userForLoginDto.Password);

            //if (userFromRepo == null)
            //    return Unauthorized();

            //var claims = new[]
            //{
            //    new Claim(ClaimTypes.NameIdentifier, userFromRepo.CustomerId.ToString()),
            //    new Claim(ClaimTypes.Email, userFromRepo.Email)
            //};

            //var key = new SymmetricSecurityKey(Encoding.UTF8
            //    .GetBytes(config.GetSection("AppSettings:SecretKey").Value));

            //var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(claims),
            //    Expires = DateTime.Now.AddDays(1),
            //    SigningCredentials = creds
            //};

            //var tokenHandler = new JwtSecurityTokenHandler();

            //var token = tokenHandler.CreateToken(tokenDescriptor);

            //return Ok(new
            //{
            //    token = tokenHandler.WriteToken(token),

            //});
            return null;
        }
    }
}
