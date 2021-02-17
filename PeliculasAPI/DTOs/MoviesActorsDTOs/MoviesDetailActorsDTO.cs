using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasAPI.DTOs.MoviesActorsDTOs
{
    public class MoviesDetailActorsDTO
    {
        public int ActorId { get; set; }
        public string Character { get; set; }
        public string ActorName { get; set; }
    }
}
