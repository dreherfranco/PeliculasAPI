using System;
using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Model.Models
{
    public class Gender
    {
        public int Id { get; set; }
        [Required]
        [StringLength(40)]
        public string Name { get; set; }
    }
}
