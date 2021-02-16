using AutoMapper;
using PeliculasAPI.DTOs.ActorsDTOs;
using PeliculasAPI.DTOs.GendersDTOs;
using PeliculasAPI.DTOs.MoviesDTOs;
using PeliculasAPI.Model.Models;

namespace PeliculasAPI.Mapper
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Gender, GenderDTO>().ReverseMap();
            CreateMap<GenderCreationDTO, Gender>();

            CreateMap<Actor, ActorDTO>().ReverseMap();
            CreateMap<Actor, ActorPatchDTO>().ReverseMap();
            CreateMap<ActorCreationDTO, Actor>()
                .ForMember(x => x.Photo, options => options.Ignore());

            CreateMap<Movie, MovieDTO>().ReverseMap();
            CreateMap<Movie, MoviePatchDTO>().ReverseMap();
            CreateMap<MovieCreationDTO, Movie>()
                .ForMember(x => x.Poster, options => options.Ignore());
        } 
    }
}
