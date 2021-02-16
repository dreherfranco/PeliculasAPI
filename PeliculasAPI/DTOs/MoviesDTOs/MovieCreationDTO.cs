using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeliculasAPI.DTOs.MoviesActorsDTOs;
using PeliculasAPI.Helpers.Validations;
using PeliculasAPI.Helpers.Validations.HelpersValidations;
using PeliculasAPI.Model.DbConfiguration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.DTOs.MoviesDTOs
{
    public class MovieCreationDTO: MoviePatchDTO
    {
        [ImageWeight(weightInMegaBytes: 6)]
        [FileType(groupFileType: GroupFileType.Image)]
        public IFormFile Poster { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> GendersIds { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<MoviesActorsCreationDTO>>))]
        public List<MoviesActorsCreationDTO> Actors { get; set; }
    }
}
