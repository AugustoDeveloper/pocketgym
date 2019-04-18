using System;
using Microsoft.AspNetCore.Mvc;

namespace PocketGym.API.Controllers
{
    public abstract class BaseApiController : ControllerBase
    {
        protected BaseApiController()
        {
        }
    }
}