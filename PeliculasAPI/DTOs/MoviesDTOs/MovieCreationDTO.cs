using Microsoft.AspNetCore.Http;
using PeliculasAPI.Helpers.Validations;
using PeliculasAPI.Helpers.Validations.HelpersValidations;
using System;
using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.DTOs.MoviesDTOs
{
    public class MovieCreationDTO: MoviePatchDTO
    {
        [ImageWeight(weightInMegaBytes: 6)]
        [FileType(groupFileType: GroupFileType.Image)]
        public IFormFile Poster { get; set; }
    }
}
