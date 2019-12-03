using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PocketGym.Application.Core.Dtos;
using PocketGym.Application.Exceptions;
using PocketGym.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PocketGym.API.Controllers.v1
{
    [Authorize(Roles = "User,Admin"), ApiController, Route("api/v1/user")]
    public class TargetController : ControllerBase
    {
        [HttpPost("{userId}/target")]
        public async Task<IActionResult> AddAsync([FromRoute] string userId, [FromBody] TargetDto target, [FromServices] ITargetApplicationService service)
        {
            if (!string.Equals(User.Identity.Name, userId))
            {
                return Forbid();
            }

            try
            {
                var registeredTarget = await service.AddAsync(userId, target);
                return CreatedAtRoute(nameof(GetTargetByIdAsync), new { userId, id = registeredTarget.Id }, registeredTarget);
            }
            catch (Application.Exceptions.ApplicationException ex)
            {
                return BadRequest(ex.ToResult());
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { reason = ex.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Problem("Something is not right, calm down calm down! We're working to fix...(I hope so!");
            }
        }

        [HttpGet("{userId}/target/{id}", Name = nameof(GetTargetByIdAsync))]
        public async Task<IActionResult> GetTargetByIdAsync([FromRoute] string userId, [FromRoute] string id, [FromServices] ITargetApplicationService service)
        {
            if (!string.Equals(User.Identity.Name, userId))
            {
                return Forbid();
            }

            var registeredTarget = await service.GetByIdAsync(userId, id);

            if (registeredTarget == null)
            {
                return NotFound();
            }

            return Ok(registeredTarget);
        }

        [HttpPatch("{userId}/target/{id}")]
        public async Task<IActionResult> MarkAsCurrentAsync([FromRoute] string userId, [FromRoute] string id, [FromServices] ITargetApplicationService service)
        {
            if (!string.Equals(User.Identity.Name, userId))
            {
                return Forbid();
            }

            if (await service.MarkAsCurrent(userId, id) == false)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{userId}/target/{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] string userId, [FromRoute] string id, [FromServices] ITargetApplicationService service)
        {
            if (!string.Equals(User.Identity.Name, userId))
            {
                return Forbid();
            }

            try
            {
                if (!await service.DeleteAsync(userId, id))
                {
                    return NotFound();
                }

                return StatusCode(204);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { reason = ex.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Problem("Something is not right, calm down calm down! We're working to fix...(I hope so!");
            }
        }
    }
}