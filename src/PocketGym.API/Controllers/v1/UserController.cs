using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PocketGym.API.Auth;
using PocketGym.Application.Core.Dtos;
using PocketGym.Application.Services;
using System;
using System.Threading.Tasks;

namespace PocketGym.API.Controllers.v1
{
    [Authorize(Roles = "User,Admin"), ApiController, Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        [AllowAnonymous, HttpPost]
        public async Task<IActionResult> RegisterAsync([FromBody] UserDto user, [FromServices] IUserApplicationService service)
        {
            try
            {
                var registeredUser = await service.CreateUserAsync(user);
                return CreatedAtRoute(nameof(GetUserByIdAsync), new { id = registeredUser.Id }, registeredUser);
            }
            catch (Application.Exceptions.ApplicationException ex)
            {
                return BadRequest(ex.ToResult());
            }
            catch(ArgumentException ex)
            {
                return BadRequest(new { reason = ex.Message });
            }
            catch(Exception ex)
            {
                return Problem("Something is not right, calm down calm down! We're working to fix...(I hope so!");
            }
        }

        [AllowAnonymous, HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] LoginDto login, [FromServices] IUserApplicationService service)
        {
            try
            {
                await service.AuthenticateAsync(login);
                login.GenerateToken();

                return Ok(login);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (ArgumentNullException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return Problem("Something is not right, calm down calm down! We're working to fix...(I hope so!");
            }
        }

        [HttpGet("{id}", Name = nameof(GetUserByIdAsync))]
        public async Task<IActionResult> GetUserByIdAsync([FromRoute] string id, [FromServices] IUserApplicationService service)
        {
            if (!string.Equals(User.Identity.Name, id))
            {
                return Forbid();
            }

            var registeredUser = await service.GetUserByIdAsync(id);

            if (registeredUser == null)
            {
                return NotFound(new { reason = $"The user id {id} not found" });
            }

            return Ok(registeredUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] string id, [FromBody] UserDto user, [FromServices] IUserApplicationService service)
        {
            if (!string.Equals(User.Identity.Name, id))
            {
                return Forbid();
            }

            try
            {
                user.Id = id;
                var updatedUser = await service.UpdateAsync(user);

                if (updatedUser == null)
                {
                    return NotFound(new { reason = $"The user id {id} not found" });
                }

                return Ok(updatedUser);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { reason = ex.Message });
            }
        }
    }
}