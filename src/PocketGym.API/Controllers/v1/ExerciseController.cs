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
    public class ExerciseController : ControllerBase
    {
        [HttpPost("{userId}/serie/{serieId}/exercise")]
        public async Task<IActionResult> AddExerciseAsync([FromRoute] string userId, [FromRoute]string serieId, [FromBody] ExerciseDto exercise, [FromServices] IExerciseApplicationService service)
        {
            if (!string.Equals(User.Identity.Name, userId))
            {
                return Forbid();
            }

            try
            {
                var registeredExercise = await service.AddAsync(userId, serieId, exercise);
                return CreatedAtRoute(nameof(GetExerciseByIdAsync), new { userId, serieId, id = registeredExercise.Id }, registeredExercise);
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


        [HttpGet("{userId}/serie/{serieId}/exercise/{id}", Name = nameof(GetExerciseByIdAsync))]
        public async Task<IActionResult> GetExerciseByIdAsync([FromRoute] string userId, [FromRoute] string serieId, [FromRoute] string id, [FromServices] IExerciseApplicationService service)
        {
            if (!string.Equals(User.Identity.Name, userId))
            {
                return Forbid();
            }

            var registeredExecise = await service.GetByIdAsync(userId, serieId, id);

            if (registeredExecise == null)
            {
                return NotFound();
            }

            return Ok(registeredExecise);
        }

        [HttpPut("{userId}/serie/{serieId}/exercise/{id}")]
        public async Task<IActionResult> UpdateExerciseAsync([FromRoute] string userId, [FromRoute] string serieId, [FromRoute] string id, [FromBody] ExerciseDto exerciseDto, [FromServices] IExerciseApplicationService service)
        {
            if (!string.Equals(User.Identity.Name, userId))
            {
                return Forbid();
            }

            try
            {
                exerciseDto.Id = id;
                var registeredExecise = await service.UpdateAsync(userId, serieId, exerciseDto);

                if (registeredExecise == null)
                {
                    return NotFound();
                }

                return Ok(registeredExecise);
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

        [HttpDelete("{userId}/serie/{serieId}/exercise/{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] string userId, [FromRoute] string serieId, [FromRoute] string id, [FromServices] IExerciseApplicationService service)
        {
            if (!string.Equals(User.Identity.Name, userId))
            {
                return Forbid();
            }

            try
            {
                if (!await service.DeleteByAsync(userId, serieId, id))
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