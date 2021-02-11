using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasAPI.DTOs.ActorsDTOs
{
    public class ActorCreationDTO
    {
        [StringLength(120)]
        [Required]
        public string Name { get; set; }
        public DateTime FechaNacimiento { get; set; }
    }
}
