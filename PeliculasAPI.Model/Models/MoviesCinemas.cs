using System;
using System.Collections.Generic;
using System.Text;

namespace PeliculasAPI.Model.Models
{
    public class MoviesCinemas
    {
        public int MovieId { get; set; }
        public int CinemaId { get; set; }
        public Cinema Cinema { get; set; }
        public Movie Movie { get; set; }
    }
}
