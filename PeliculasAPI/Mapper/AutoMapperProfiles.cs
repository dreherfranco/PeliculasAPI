using AutoMapper;
using PeliculasAPI.DTOs.ActorsDTOs;
using PeliculasAPI.DTOs.CinemasDTO;
using PeliculasAPI.DTOs.GendersDTOs;
using PeliculasAPI.DTOs.MoviesActorsDTOs;
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

            CreateMap<Cinema,CinemaDTO>().ReverseMap();
            CreateMap<CinemaCreationDTO, Cinema>();

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

            CreateMap<Movie, MovieDetailsDTO>()
                .ForMember(x => x.Actors, options => options.MapFrom(this.MapActorDetails))
                .ForMember(x => x.Genders, options => options.MapFrom(this.MapGenderDetails));
        } 

        private List<MoviesGenders> MapMoviesGenders(MovieCreationDTO movieCreationDto, Movie movie)
        {
            var result = new List<MoviesGenders>();
            if(movieCreationDto.GendersIds == null) { return result; }

            foreach(var id in movieCreationDto.GendersIds)
            {
                result.Add(new MoviesGenders() { GenderId = id });
            }
            return result;
        }

        private List<MoviesActors> MapMoviesActors(MovieCreationDTO movieCreationDto, Movie movie)
        {
            var result = new List<MoviesActors>();
            if (movieCreationDto.Actors == null) { return result; }

            foreach (var actor in movieCreationDto.Actors)
            {
                result.Add(new MoviesActors() { ActorId = actor.ActorId, Character = actor.Character });
            }
            return result;
        }

        private List<MoviesDetailActorsDTO> MapActorDetails(Movie movie, MovieDetailsDTO movieDetailDTO)
        {
            var result = new List<MoviesDetailActorsDTO>();
            if(movie.MoviesActors == null) { return result; }

            foreach(var actor in movie.MoviesActors)
            {
                result.Add(new MoviesDetailActorsDTO ()
                { 
                    ActorId = actor.ActorId, ActorName = actor.Actor.Name, Character = actor.Character 
                } );
            }
            return result;
        }

        private List<GenderDTO> MapGenderDetails(Movie movie, MovieDetailsDTO movieDetailDTO)
        {
            var result = new List<GenderDTO>();
            if (movie.MoviesGenders == null) { return result; }

            foreach (var gender in movie.MoviesGenders)
            {
                result.Add(new GenderDTO()
                {
                    Id = gender.GenderId,
                    Name = gender.Gender.Name
                });
            }
            return result;
        }
    }
}
