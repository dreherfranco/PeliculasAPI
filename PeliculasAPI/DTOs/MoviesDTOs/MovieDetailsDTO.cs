using PeliculasAPI.DTOs.GendersDTOs;
using PeliculasAPI.DTOs.MoviesActorsDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasAPI.DTOs.MoviesDTOs
{
    public class MovieDetailsDTO: MovieDTO
    {
        public List<MoviesDetailActorsDTO> Actors { get; set; }
        public List<GenderDTO> Genders { get; set; }
    }
}
