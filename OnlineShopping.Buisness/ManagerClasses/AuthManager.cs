using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnlineShopping.Buisness.DTOs;
using OnlineShopping.Buisness.ManagerClasses;
using OnlineShopping.Common;
using OnlineShopping.DataAccess.Models;
using OnlineShopping.DataAccess.Repository;
using Enum = OnlineShopping.Common.Enum;

namespace OnlineShopping.Buisness.ManagerClasses
{
    public class AuthManager 
    {
        //UserForRegisterDto userForRegisterDto = new UserForRegisterDto();
        //AuthRepository repo = new AuthRepository();
        //AuthRepository repo;
       
        AuthRepository repo = new AuthRepository();
        private readonly IConfiguration config;

        public AuthManager()
        {
             //this.repo= new AuthRepository();
        }

        public AuthManager(IConfiguration config)
        {
            this.config = config;
        }


        public async Task<OperationResult> UserRegister(UserForRegisterDto userForRegisterDto)
        {

            OperationResult operationResult = new OperationResult();
            //operationResult.Status = Enum.Status.Success;
            //operationResult.Message = Constant.SuccessMessage;
            //operationResult.Message = Constant.UserExisits;
           

            userForRegisterDto.Email = userForRegisterDto.Email.ToLower();

            if (await repo.UserExists(userForRegisterDto.Email))
            {
                operationResult.Message = Constant.UserExisits;
                return operationResult;
            }
                

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

            if (createdUser != null)
            {
                operationResult.Data = createdUser;

                operationResult.Message = "successfully created";
            }
            else
            {
                operationResult.Message = "Not user created";
            }

            return operationResult;

            

            //return StatusCode(201);

        }

        public async Task<OperationResult> UserLogin(UserForLoginDto userForLoginDto)
        {
            //UserForLoginDto userForLoginDto = new UserForLoginDto();
            OperationResult operationResult = new OperationResult();
            operationResult.Status = Enum.Status.Success;
            operationResult.Message = Constant.SuccessMessage;
            operationResult.Message = Constant.UserExisits;

            var userFromRepo = await repo.Login(userForLoginDto.Email, userForLoginDto.Password);

            if (userFromRepo == null)
                return null;

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.CustomerId.ToString()),
                new Claim(ClaimTypes.Email, userFromRepo.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(config.GetSection("AppSettings:SecretKey").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);
            //new token = tokenHandler.WriteToken(token);

            //return Ok(new
            //{
            //    token = tokenHandler.WriteToken(token),

            //});
            return operationResult.Data(token);


        }
    }
}
