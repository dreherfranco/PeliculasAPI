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
    public class ActorCreationDTO: ActorPatchDTO
    {
        [ImageWeight(weightInMegaBytes:6)]
        [FileType(groupFileType: GroupFileType.Image)]
        public IFormFile Photo { get; set; }
    }
}
