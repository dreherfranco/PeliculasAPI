using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PeliculasAPI.Model.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        [StringLength(120)]
        public string Title { get; set; }
        public bool InTheaters { get; set; }
        public DateTime PremiereDate { get; set; }
        public string Poster { get; set; }
    }

}
