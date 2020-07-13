using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OnlineShopping.Buisness.DTOs;
using OnlineShopping.Buisness.Manager;
using OnlineShopping.DataAccess.Repository;

namespace OnlineShopping.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Auth2Controller : ControllerBase
    {
        private readonly IAuthRepository repo;
        private readonly Auth2Manager auth;

        public Auth2Controller(IAuthRepository repo, Auth2Manager auth)
        {
            this.repo = repo;
            this.auth = auth;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            userForRegisterDto.Email = userForRegisterDto.Email.ToLower();

            if (await repo.UserExists(userForRegisterDto.Email))
                return BadRequest("User exists");
           
            var cr =  auth.Register(userForRegisterDto);
            var cre = await repo.Register(cr, userForRegisterDto.Password);

            //return StatusCode(201);
            return Ok(cre);

        }

    }
}
