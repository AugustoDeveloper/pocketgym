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
    [Authorize, ApiController, Route("api/[controller]")]
    public class SerieController : BaseApiController
    {
        public SerieController(IOptions<AppSettings> settings) : base(settings.Value)
        {
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(
            [FromServices] ISerieApplicationService service,
            [FromBody] SerieDto serie)
        {
            using (service)
            {
                if (serie == null)
                {
                    return BadRequest(new
                    {
                        Reason = "The serie is invalid"
                    });
                }

                if (serie.UserId != CurrentUserId)
                {
                    return Forbid();
                }

                try
                {
                    var newSerie = await service.AddAsync(serie);
                    if (newSerie == null)
                    {
                        return BadRequest(new
                        {
                            Reason = "The serie is not ok"
                        });
                    }

                    return CreatedAtAction(nameof(GetByIdAsync), new { newSerie.Id }, newSerie);
                }
                catch (ValueAlreadyRegisteredException ex)
                {
                    return BadRequest(new { Reason = ex.Message });
                }
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromServices] ISerieApplicationService service, long id)
        {
            using (service)
            {
                var serie = await service.GetByIdAsync(id);
                if (serie == null)
                {
                    return NotFound(new { Reason = "Serie not found" });
                }

                if (serie.UserId != CurrentUserId)
                {
                    return Forbid();
                }

                return Ok(serie);
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> LoadAllAsync([FromServices] ISerieApplicationService service)
        {
            using (service)
            {
                var series = await service.LoadAllByUserIdAsync(CurrentUserId);
                if (series == null)
                {
                    return NotFound(new { Reason = "Series not found" });
                }

                return Ok(series);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync([FromServices] ISerieApplicationService service, long id)
        {
            using (service)
            {
                var serie = await service.GetByIdAsync(id);
                if (serie == null)
                {
                    return NotFound(new { Reason = "Serie not found" });
                }

                if (serie.UserId != CurrentUserId)
                {
                    return Forbid();
                }

                await service.DeleteAsync(serie);

                return Ok();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(
            [FromServices] ISerieApplicationService service,
            [FromBody] SerieDto serie,
            long id)
        {
            using (service)
            {
                if (serie == null)
                {
                    return BadRequest(new
                    {
                        Id = id,
                        Reason = "The serie is invalid"
                    });
                }


                if (serie.Id != id)
                {
                    return BadRequest(new
                    {
                        Id = id,
                        Serie = serie,
                        Reason = "The serie is invalid"
                    });
                }

                if (serie.UserId != CurrentUserId)
                {
                    return Forbid();
                }

                try
                {
                    var newSerie = await service.UpdateAsync(serie);
                    if (newSerie == null)
                    {
                        return BadRequest(new
                        {
                            Id = id,
                            Reason = "The serie not exists"
                        });
                    }

                    return Ok(newSerie);
                }
                catch (ValueAlreadyRegisteredException ex)
                {
                    return BadRequest(new { Reason = ex.Message });
                }
            }
        }
    }
}