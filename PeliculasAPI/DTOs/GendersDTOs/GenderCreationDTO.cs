using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasAPI.DTOs.GendersDTOs
{
    public class GenderCreationDTO
    {
        [Required]
        [StringLength(40)]
        public string Name { get; set; }
    }
}
