using AutoMapper;
using PeliculasAPI.DTOs.ActorsDTOs;
using PeliculasAPI.DTOs.GendersDTOs;
using PeliculasAPI.DTOs.MoviesDTOs;
using PeliculasAPI.Model.Models;
using System.Collections.Generic;

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
                .ForMember(x => x.Poster, options => options.Ignore())
                .ForMember(x => x.MoviesGenders, options => options.MapFrom(this.MapMoviesGenders))
                .ForMember(x => x.MoviesActors, options=> options.MapFrom(this.MapMoviesActors));
        } 

        private List<MoviesGenders> MapMoviesGenders(MovieCreationDTO movieCreationDto, Movie movie)
        {
            var result = new List<MoviesGenders>();
            if(movieCreationDto.GendersIds == null) { return result; }

            foreach(var id in movieCreationDto.GendersIds)
            {
                result.Add(new MoviesGenders { GenderId = id });
            }
            return result;
        }

        private List<MoviesActors> MapMoviesActors(MovieCreationDTO movieCreationDto, Movie movie)
        {
            var result = new List<MoviesActors>();
            if (movieCreationDto.Actors == null) { return result; }

            foreach (var actor in movieCreationDto.Actors)
            {
                result.Add(new MoviesActors { ActorId = actor.ActorId, Character = actor.Character });
            }
            return result;
        }

    }
}
