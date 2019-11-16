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
    public class SerieController : ControllerBase
    {
        [HttpPost("{userId}/serie")]
        public async Task<IActionResult> AddSerieAsync([FromRoute] string userId, [FromBody] SerieDto serie, [FromServices] ISerieApplicationService service)
        {
            if (!string.Equals(User.Identity.Name, userId))
            {
                return Forbid();
            }

            try
            {
                var registeredUser = await service.AddAsync(userId, serie);
                return CreatedAtRoute(nameof(GetSerieByIdAsync), new { userId, id = registeredUser.Id }, registeredUser);
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
                return Problem("Something is not right, calm down calm down! We're working to fix...(I hope so!");
            }
        }


        [HttpGet("{userId}/serie/{id}", Name = nameof(GetSerieByIdAsync))]
        public async Task<IActionResult> GetSerieByIdAsync([FromRoute] string userId, [FromRoute] string id, [FromServices] ISerieApplicationService service)
        {
            if (!string.Equals(User.Identity.Name, userId))
            {
                return Forbid();
            }

            var registeredUser = await service.GetByIdAsync(userId, id);

            if (registeredUser == null)
            {
                return NotFound();
            }

            return Ok(registeredUser);
        }

        [HttpPut("{userId}/serie/{id}")]
        public async Task<IActionResult> UpdateSerieAsync([FromRoute] string userId, [FromRoute] string id, [FromBody] SerieDto serie, [FromServices] ISerieApplicationService service)
        {
            if (!string.Equals(User.Identity.Name, userId))
            {
                return Forbid();
            }

            try
            {
                serie.Id = id;
                var registeredUser = await service.UpdateAsync(userId, serie);

                if (registeredUser == null)
                {
                    return NotFound();
                }

                return Ok(registeredUser);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { reason = ex.Message });
            }
            catch (Exception ex)
            {
                return Problem("Something is not right, calm down calm down! We're working to fix...(I hope so!");
            }
        }

        [HttpDelete("{userId}/serie/{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] string userId, [FromRoute] string id, [FromServices] ISerieApplicationService service)
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
                return Problem("Something is not right, calm down calm down! We're working to fix...(I hope so!");
            }
        }
    }
}