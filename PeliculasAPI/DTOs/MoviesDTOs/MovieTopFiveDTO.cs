using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasAPI.DTOs.MoviesDTOs
{
    public class MovieTopFiveDTO
    {
        public List<MovieDTO> InTheaters { get; set; }
        public List<MovieDTO> NextReleases { get; set; }
    }
}
