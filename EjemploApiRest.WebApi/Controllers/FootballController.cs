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
        private readonly IApplication<FootballTeamDto> _football;

        public FootballController(IApplication<FootballTeamDto> football)
        {
            _football = football;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_football.GetAll());
        }

        [HttpPost]
        public IActionResult Save(FootballTeamDto dto)
        {
            var item = new FootballTeamDto()
            {
                Name = dto.Name,
                Score = dto.Score
            };
            return Ok(_football.Save(item));
        }
    }
}
