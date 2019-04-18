using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PocketGym.Application.Core.Dtos;
using PocketGym.Application.Services;

namespace PocketGym.API.Controllers
{
    [ApiController, Route("user")]
    public class UserController : BaseApiController
    {
        private readonly IUserApplicationService userService;

        public UserController(IUserApplicationService userService) : base()
        {
            this.userService = userService;
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateUserAsync([FromBody]UserDto user)
        {
            var createdUser = await userService.CreateUserAsync(user);
            if (createdUser == null)
            {
                return BadRequest(user);
            }

            return CreatedAtAction(nameof(GetUserAsync), new { id = createdUser.Id }, createdUser);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserAsync(long id)
        {
            var user = await userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound(id);
            }

            return Ok(user);
        }
    }
}