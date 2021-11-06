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
        [HttpPost("{userId}/target/{targetId}/serie")]
        public async Task<IActionResult> AddSerieAsync([FromRoute] string userId, [FromRoute] string targetId, [FromBody] SerieDto serie, [FromServices] ISerieApplicationService service)
        {
            if (!string.Equals(User.Identity.Name, userId))
            {
                return Forbid();
            }

            try
            {
                var registeredSerie = await service.AddAsync(userId, targetId, serie);
                return CreatedAtRoute(nameof(GetSerieByIdAsync), new { userId, targetId, id = registeredSerie.Id }, registeredSerie);
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


        [HttpGet("{userId}/target/{targetId}/serie/{id}", Name = nameof(GetSerieByIdAsync))]
        public async Task<IActionResult> GetSerieByIdAsync([FromRoute] string userId, [FromRoute] string targetId, [FromRoute] string id, [FromServices] ISerieApplicationService service)
        {
            if (!string.Equals(User.Identity.Name, userId))
            {
                return Forbid();
            }

            var registeredSerie = await service.GetByIdAsync(userId, targetId, id);

            if (registeredSerie == null)
            {
                return NotFound();
            }

            return Ok(registeredSerie);
        }

        [HttpDelete("{userId}/target/{targetId}/serie/{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] string userId, [FromRoute] string targetId, [FromRoute] string id, [FromServices] ISerieApplicationService service)
        {
            if (!string.Equals(User.Identity.Name, userId))
            {
                return Forbid();
            }

            try
            {
                if (!await service.DeleteAsync(userId, targetId, id))
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