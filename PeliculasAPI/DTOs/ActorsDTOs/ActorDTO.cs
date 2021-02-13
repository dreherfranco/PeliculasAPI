using Microsoft.AspNetCore.Http;
using PeliculasAPI.Helpers.Validations;
using PeliculasAPI.Helpers.Validations.HelpersValidations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasAPI.DTOs.ActorsDTOs
{
    public class ActorDTO
    {
        public int Id { get; set; }
        [StringLength(120)]
        [Required]
        public string Name { get; set; }
        public DateTime Birthday { get; set; }

        [ImageWeight(weightInMegaBytes: 6)]
        [FileType(groupFileType: GroupFileType.Image)]
        public string Photo { get; set; }
    }
}
