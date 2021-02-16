using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasAPI.DTOs.MoviesDTOs
{
    public class MovieDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool InTheaters { get; set; }
        public DateTime PremiereDate { get; set; }
        public string Poster { get; set; }
    }
}
