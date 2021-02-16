﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PeliculasAPI.Model.Models
{
    public class MoviesGenders
    {
        public int GenderId { get; set; }
        public int MovieId { get; set; }
        public Gender Gender { get; set; }
        public Movie Movie { get; set; }
    }
}
