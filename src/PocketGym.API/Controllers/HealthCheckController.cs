using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PocketGym.Application.Services;

namespace PocketGym.API.Controllers
{
    [ApiController, AllowAnonymous, Route("api/[controller]")]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromServices] IHealthCheckApplicationService service)
        {
            if (service.Check()) 
            {
                return await Task.FromResult(Ok());
            }

             return await Task.FromResult(Problem());
        }

        [HttpGet("details")]
        public async Task<IActionResult> GetDetailsAsync([FromServices] IHealthCheckApplicationService service)
        {
            var result = service.GetDetails();
            if (result.Success) 
            {
                return await Task.FromResult(Ok(result));
            }

             return await Task.FromResult(Problem("Oh, god! Why!? Why!?"));
        }
    }
}