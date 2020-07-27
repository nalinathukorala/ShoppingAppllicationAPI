using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OnlineShopping.Buisness.DTOs;
using OnlineShopping.DataAccess.Repository;
using OnlineShopping.Buisness.ManagerClasses;
using OnlineShopping.Common;

namespace OnlineShopping.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Auth2Controller : ControllerBase
    {
        private readonly AuthManager authManager;
        private readonly IConfiguration config;

        //AuthManager authManager = new AuthManager();
        public Auth2Controller(IConfiguration config)
        {
            this.authManager = new AuthManager();
            this.config = config;
            //UserForRegisterDto userForRegisterDto = new UserForRegisterDto();

        }

        //user registration API endpoint
        [HttpPost("register")]
        public async Task<OperationResult> Register(UserForRegisterDto userForRegisterDto)
        {
            OperationResult operationResult = await authManager.UserRegister(userForRegisterDto);
            return operationResult;

        }

        //user login API endpoint
        [HttpPost("login")]
        public async Task<OperationResult> Login(UserForLoginDto userForLoginDto)
        {
            OperationResult operationResult = await authManager.UserLogin(userForLoginDto);
            return operationResult;
        }

    }
}
