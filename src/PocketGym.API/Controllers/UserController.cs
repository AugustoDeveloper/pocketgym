using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PocketGym.API.Configurations;
using PocketGym.Application.Core.Dtos;
using PocketGym.Application.Exceptions;
using PocketGym.Application.Services;

namespace PocketGym.API.Controllers
{
    [Authorize("Bearer"), ApiController, Route("api/[controller]")]
    public class UserController : BaseApiController
    {
        private readonly IUserApplicationService userService;

        public UserController(IUserApplicationService userService, IOptions<AppSettings> settings) : base(settings.Value)
        {
            this.userService = userService;
        }

        [AllowAnonymous, HttpPost("")]
        public async Task<IActionResult> RegiterAsync([FromBody]UserDto user)
        {
            try
            {
                var createdUser = await userService.CreateUserAsync(user);

                return CreatedAtAction(nameof(GetUserAsync), new { id = createdUser.Id }, createdUser);
            }
            catch(ValueAlreadyRegisteredException ex)
            {
                return BadRequest(new { User = user, Reason = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserAsync(long id)
        {
            var user = await userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound(new
                {
                    Id = id,
                    Reason = "The user id not found"
                });
            }

            long.TryParse(User.Identity.Name, out long currentId);

            if (id != currentId)
            {
                return Forbid();
            }

            return Ok(user);
        }
    }
}