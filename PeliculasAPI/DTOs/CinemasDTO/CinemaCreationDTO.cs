using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasAPI.DTOs.CinemasDTO
{
    public class CinemaCreationDTO
    {
        [Required]
        [StringLength(120)]
        public string Name { get; set; }
    }
}
