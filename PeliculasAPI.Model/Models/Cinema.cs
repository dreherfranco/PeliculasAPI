using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using PeliculasAPI.Model.Models.Interfaces;
using NetTopologySuite.Geometries;

namespace PeliculasAPI.Model.Models
{
    public class Cinema: IEntity
    {
        public int Id { get; set; }
        [Required]
        [StringLength(120)]
        public string Name { get; set; }
        public Point Ubication { get; set; }
        public List<MoviesCinemas> MoviesCinemas { get; set; }
    }
}
