using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HOPE_13.Data;
using HOPE_13.Dtos;
using HOPE_13.Models;
using Microsoft.AspNetCore.Mvc;

namespace HOPE_13.Controllers
{
   [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        public AccountController(IAuthRepository repo)
        {
            _repo=repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            userForRegisterDto.UserName = userForRegisterDto.UserName.ToLower();

            if(await _repo.UserExists(userForRegisterDto.UserName))
              return BadRequest("Користувач уже ісує");

            var userToCreate = new User
            {
                UserName = userForRegisterDto.UserName
            };
            var createdUser = await _repo.Register(userToCreate, userForRegisterDto.Password);

            return StatusCode(201);
        }
    }

}

