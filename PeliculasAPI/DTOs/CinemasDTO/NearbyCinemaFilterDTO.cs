using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasAPI.DTOs.CinemasDTO
{
    public class NearbyCinemaFilterDTO
    {
        [Range(-90,90)]
        public double Latitude { get; set; }
        [Range(-180, 180)]
        public double Longitude { get; set; }
        private int distanceInKms = 10;
        private int maximumDistanceInKms = 50;

        public int DistanceInKms
        {
            get => distanceInKms;
            set
            {
                distanceInKms = (value > maximumDistanceInKms) ? maximumDistanceInKms : value;
            }
        }


    }
}
