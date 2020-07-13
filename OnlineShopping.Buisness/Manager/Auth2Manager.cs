using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnlineShopping.Buisness.DTOs;
using OnlineShopping.DataAccess.Models;
using OnlineShopping.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.Buisness.Manager
{
   public class Auth2Manager
    {
        private readonly IAuthRepository repo;
        private readonly IConfiguration config;

        public Auth2Manager(IAuthRepository repo, IConfiguration config)
        {
            this.repo = repo;
            this.config = config;
        }

        public Customers Register(UserForRegisterDto userForRegisterDto)
        {
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

            return userToCreate;
        }

        public async Task<Customers> Login(UserForLoginDto userForLoginDto)
        {

            var userFromRepo = await repo.Login(userForLoginDto.Email, userForLoginDto.Password);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.CustomerId.ToString()),
                new Claim(ClaimTypes.Email, userFromRepo.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return userFromRepo;

            //return (new
            //{
            //    token = tokenHandler.WriteToken(token)
            //});
        }

    }
}
