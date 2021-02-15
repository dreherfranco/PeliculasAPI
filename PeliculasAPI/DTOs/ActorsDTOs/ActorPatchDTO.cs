using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasAPI.DTOs.ActorsDTOs
{
    public class ActorPatchDTO
    {
        [StringLength(120)]
        [Required]
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
    }
}
