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
    public class ExerciseStepController : ControllerBase
    {
        [HttpPost("{userId}/target/{targetId}/serie/{serieId}/exercise/single")]
        public Task<IActionResult> AddSingleExerciseStepAsync(
            [FromRoute] string userId, 
            [FromRoute] string targetId, 
            [FromRoute]string serieId, 
            [FromBody] SingleExerciseStepDto exercise, 
            [FromServices] IExerciseStepApplicationService service)

         => AddExerciseStepAsync(userId, targetId, serieId, exercise, service);

         [HttpPost("{userId}/target/{targetId}/serie/{serieId}/exercise/dropset")]
        public Task<IActionResult> AddDropSetExerciseStepAsync(
            [FromRoute] string userId, 
            [FromRoute] string targetId, 
            [FromRoute]string serieId, 
            [FromBody] DropSetExerciseStepDto exercise, 
            [FromServices] IExerciseStepApplicationService service)

         => AddExerciseStepAsync(userId, targetId, serieId, exercise, service);

         [HttpPost("{userId}/target/{targetId}/serie/{serieId}/exercise/conjugate")]
        public Task<IActionResult> AddConjugateExerciseStepAsync(
            [FromRoute] string userId, 
            [FromRoute] string targetId, 
            [FromRoute]string serieId, 
            [FromBody] ConjugateExerciseStepDto exercise, 
            [FromServices] IExerciseStepApplicationService service)

         => AddExerciseStepAsync(userId, targetId, serieId, exercise, service);

        private async Task<IActionResult> AddExerciseStepAsync(string userId, string targetId, string serieId, IExerciseStepDto exercise, IExerciseStepApplicationService service)
        {
            if (!string.Equals(User.Identity.Name, userId))
            {
                return Forbid();
            }

            try
            {
                var registeredExercise = await service.AddAsync(userId, targetId, serieId, exercise);
                return CreatedAtRoute(nameof(GetExerciseStepByIdAsync), new { userId, targetId, serieId, id = registeredExercise.Id }, registeredExercise);
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


        [HttpGet("{userId}/target/{targetId}/serie/{serieId}/exercise/{id}", Name = nameof(GetExerciseStepByIdAsync))]
        public async Task<IActionResult> GetExerciseStepByIdAsync([FromRoute] string userId, [FromRoute] string targetId,  [FromRoute] string serieId, [FromRoute] string id, [FromServices] IExerciseStepApplicationService service)
        {
            if (!string.Equals(User.Identity.Name, userId))
            {
                return Forbid();
            }

            var registeredExecise = await service.GetByIdAsync(userId, targetId, serieId, id);

            if (registeredExecise == null)
            {
                return NotFound();
            }

            return Ok(registeredExecise);
        }

        [HttpDelete("{userId}/target/{targetId}/serie/{serieId}/exercise/{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] string userId, [FromRoute] string targetId,  [FromRoute] string serieId, [FromRoute] string id, [FromServices] IExerciseStepApplicationService service)
        {
            if (!string.Equals(User.Identity.Name, userId))
            {
                return Forbid();
            }

            try
            {
                if (!await service.DeleteByAsync(userId, targetId, serieId, id))
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