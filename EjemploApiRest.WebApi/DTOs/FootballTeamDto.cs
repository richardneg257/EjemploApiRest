﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EjemploApiRest.WebApi.DTOs
{
    public class FootballTeamDto
    {
        public string Name { get; set; }
        public double Score { get; set; }
        public string Manager { get; set; }
    }
}
