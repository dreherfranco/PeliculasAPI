using System;
using System.Collections.Generic;
using System.Text;

namespace PeliculasAPI.Model.Models
{
    public class MoviesActors
    {
        public int MovieId { get; set; }
        public int ActorId { get; set; }
        public string Character { get; set; }
        public int Order { get; set; }
        public Actor Actor{ get; set; }
        public Movie Movie { get; set; }
    }
}
