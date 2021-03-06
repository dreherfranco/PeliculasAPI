﻿using Microsoft.AspNetCore.Http;
using PeliculasAPI.Helpers.Validations;
using PeliculasAPI.Helpers.Validations.HelpersValidations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasAPI.DTOs.ActorsDTOs
{
    public class ActorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string Photo { get; set; }
    }
}
