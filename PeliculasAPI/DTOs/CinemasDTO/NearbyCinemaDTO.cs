using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasAPI.DTOs.CinemasDTO
{
    public class NearbyCinemaDTO: CinemaDTO
    {
        public double DistanceInMts { get; set; }
    }
}
