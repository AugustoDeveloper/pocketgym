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
    [Authorize(Roles = "User,Admin"), ApiController, Route("api/v1/exercise")]
    public class ExerciseController : ControllerBase
    {
        [Authorize(Roles = "Admin"), HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] ExerciseDto exercise, [FromServices] IExerciseApplicationService service)
        {

            try
            {
                var registeredExercise = await service.AddAsync(exercise);
                return CreatedAtRoute(nameof(GetExerciseByIdAsync), new { id = registeredExercise.Id }, registeredExercise);
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
        
        [HttpGet("{id}", Name = nameof(GetExerciseByIdAsync))]
        public async Task<IActionResult> GetExerciseByIdAsync([FromRoute] string id, [FromServices] IExerciseApplicationService service)
        {
            var registeredExecise = await service.GetByIdAsync(id);

            if (registeredExecise == null)
            {
                return NotFound();
            }

            return Ok(registeredExecise);
        }

        [HttpGet("all")]
        public async Task<IActionResult> LoadAll([FromRoute] string id, [FromServices] IExerciseApplicationService service)
        {
            var exercises = await service.LoadAllAsync();

            return Ok(exercises);
        }

        
        [Authorize(Roles = "Admin"), HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] string id, [FromServices] IExerciseApplicationService service)
        {
            try
            {
                if (!await service.DeleteByAsync(id))
                {
                    return NotFound();
                }

                return NoContent();
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