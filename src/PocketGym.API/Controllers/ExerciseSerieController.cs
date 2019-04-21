using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PocketGym.API.Configurations;
using PocketGym.Application.Core.Dtos;
using PocketGym.Application.Services;

namespace PocketGym.API.Controllers
{
    [Authorize("Bearer"), ApiController, Route("api/serie/{serieId}/exercise", Name = "ExerciseSerie")]
    public class ExerciseSerieController : BaseApiController
    {
        public ExerciseSerieController(IOptions<AppSettings> settings) : base(settings.Value)
        {
        }

        [HttpGet("all")]
        public async Task<IActionResult> LoadAllBySeriesAsync(
            [FromServices] IExerciseSerieApplicationService service,
            [FromServices] ISerieApplicationService serieService,
            long serieId)
        {
            var serie = await serieService.GetByIdAsync(serieId);
            if (serie == null)
            {
                return NotFound(new { Reason = "Serie not found" });
            }

            if (serie.UserId != CurrentUserId)
            {
                return Forbid();
            }

            var list = await service.LoadAllBySerieAsync(serie);

            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetyByIdAsync(
            [FromServices] IExerciseSerieApplicationService service,
            [FromServices] ISerieApplicationService serieService,
            long serieId,
            long id)
        {
            var serie = await serieService.GetByIdAsync(serieId);
            if (serie == null)
            {
                return NotFound(new { Reason = "Serie not found" });
            }

            if (serie.UserId != CurrentUserId)
            {
                return Forbid();
            }

            var serieExercise = await service.GetByIdAsync(serie, id);

            if (serie == null)
            {
                return NotFound(new { Reason = "Exercise serie not found" });
            }

            return Ok(serieExercise);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(
            [FromServices] IExerciseSerieApplicationService service,
            [FromServices] ISerieApplicationService serieService,
            [FromBody] ExerciseSerieDto exerciseSerie,
            long serieId)
        {
            var serie = await serieService.GetByIdAsync(serieId);
            if (serie == null)
            {
                return NotFound(new { Reason = "Serie not found" });
            }

            if (exerciseSerie == null)
            {
                return BadRequest(new { Reason = "Excercise serie is invalid" });
            }

            if (serie.UserId != CurrentUserId)
            {
                return Forbid();
            }

            var serieExercise = await service.AddAsync(serie, exerciseSerie);


            return CreatedAtAction(nameof(GetyByIdAsync), new { serieId, id = serieExercise.Id }, serieExercise);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(
            [FromServices] IExerciseSerieApplicationService service,
            [FromServices] ISerieApplicationService serieService,
            [FromBody] ExerciseSerieDto exerciseSerie,
            long serieId,
            long id)
        {
            var serie = await serieService.GetByIdAsync(serieId);
            if (serie == null)
            {
                return NotFound(new { Reason = "Serie not found" });
            }

            if (exerciseSerie == null)
            {
                return BadRequest(new { Reason = "Excercise serie is invalid" });
            }

            if (exerciseSerie.Id != id)
            {
                return BadRequest(new { Reason = "Excercise serie is invalid" });
            }

            if (serie.UserId != CurrentUserId)
            {
                return Forbid();
            }

            var serieExercise = await service.UpdateAsync(serie, exerciseSerie);
            if (serie == null)
            {
                return NotFound(new { Reason = "Exercise serie not found" });
            }

            return Ok(serieExercise);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(
            [FromServices] IExerciseSerieApplicationService service,
            [FromServices] ISerieApplicationService serieService,
            long serieId,
            long id)
        {
            var serie = await serieService.GetByIdAsync(serieId);
            if (serie == null)
            {
                return NotFound(new { Reason = "Serie not found" });
            }

            if (serie.UserId != CurrentUserId)
            {
                return Forbid();
            }

            await service.DeleteByIdAsync(serie, id);

            return Ok();
        }
    }
}