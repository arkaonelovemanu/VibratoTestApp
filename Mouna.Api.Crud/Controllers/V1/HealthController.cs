using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mouna.Api.Crud.Controllers.V1
{
    [Route("api/V1/[controller]")]
    public class HealthController : Controller
    {
        // GET: api/Health
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Status healthy");
        }

        // GET: api/Health/5

    }
}
