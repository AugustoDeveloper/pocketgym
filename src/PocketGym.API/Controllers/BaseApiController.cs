using System;
using Microsoft.AspNetCore.Mvc;
using PocketGym.API.Configurations;

namespace PocketGym.API.Controllers
{
    public abstract class BaseApiController : ControllerBase
    {
        protected AppSettings Settings { get; set; }
        protected long CurrentUserId => long.Parse(User.Identity.Name);

        protected BaseApiController(AppSettings settings)
        {
            this.Settings = settings;
        }
    }
}