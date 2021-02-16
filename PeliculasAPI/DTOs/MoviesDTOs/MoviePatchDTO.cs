using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasAPI.DTOs.MoviesDTOs
{
    public class MoviePatchDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(120)]
        public string Title { get; set; }
        public bool InTheaters { get; set; }
        public DateTime PremiereDate { get; set; }
    }
}
