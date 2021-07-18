using EjemploApiRest.Application;
using EjemploApiRest.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EjemploApiRest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FootballController : ControllerBase
    {
        private readonly IApplication<FootballTeam> _football;

        public FootballController(IApplication<FootballTeam> football)
        {
            _football = football;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new FootballTeam()
            {
                Name = "San Lorenzo",
                Score = 100
            });
        }
    }
}
