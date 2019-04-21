using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PocketGym.API.Configurations;
using PocketGym.Application.Core.Dtos;
using PocketGym.Application.Exceptions;
using PocketGym.Application.Services;

namespace PocketGym.API.Controllers
{
    [Authorize("Bearer", Roles = "Admin"), 
        ApiController, 
        Route("api/[controller]")]
    public class ExerciseController : BaseApiController
    {
        public ExerciseController(IOptions<AppSettings> settings) : base(settings.Value)
        {
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(
            [FromServices] IExerciseApplicationService service,
            [FromBody]ExerciseDto exercise)
        {
            if (exercise == null)
            {
                return BadRequest(new { Reason = "Exercise is invalid" });
            }

            try
            {
                var newExercise = await service.AddAsync(exercise);
                if (newExercise == null)
                {
                    return BadRequest(new
                    {
                        Reason = "The exercise is not ok"
                    });
                }

                return CreatedAtAction(nameof(GetByIdAsync), new { newExercise.Id }, newExercise);
            }
            catch (ValueAlreadyRegisteredException ex)
            {
                return BadRequest(new { Reason = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(
            [FromServices] IExerciseApplicationService service,
            [FromBody] ExerciseDto exercise,
            long id)
        {
            if (exercise == null)
            {
                return BadRequest(new
                {
                    Id = id,
                    Reason = "The exercise is invalid"
                });
            }


            if (exercise.Id != id)
            {
                return BadRequest(new
                {
                    Id = id,
                    Exercise = exercise,
                    Reason = "The exercise is invalid"
                });
            }

            try
            {
                var newExercise = await service.UpdateAsync(exercise);
                if (newExercise == null)
                {
                    return BadRequest(new
                    {
                        Id = id,
                        Reason = "The exercise not exists"
                    });
                }

                return Ok(newExercise);
            }
            catch (ValueAlreadyRegisteredException ex)
            {
                return BadRequest(new { Reason = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync([FromServices] IExerciseApplicationService service, long id)
        {
            var exercise = await service.GetByIdAsync(id);
            if (exercise == null)
            {
                return NotFound(new { Reason = "Exercise not found" });
            }

            await service.DeleteAsync(exercise);

            return Ok();
        }

        [Authorize("Bearer"), HttpGet("all")]
        public async Task<IActionResult> LoadAllAsync([FromServices] IExerciseApplicationService service)
        {
            var exercises = await service.LoadAllAsync();
            if (exercises == null)
            {
                return NotFound(new { Reason = "Series not found" });
            }

            return Ok(exercises);
        }


        [Authorize("Bearer"), HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(long id, [FromServices] IExerciseApplicationService service)
        {
            var exercise = await service.GetByIdAsync(id);
            if (exercise == null)
            {
                return NotFound(new { Reason = "Serie not found" });
            }

            return Ok(exercise);
        }
    }
}