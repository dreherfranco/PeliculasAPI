using AutoMapper;
using NetTopologySuite.Geometries;
using PeliculasAPI.DTOs.ActorsDTOs;
using PeliculasAPI.DTOs.CinemasDTO;
using PeliculasAPI.DTOs.GendersDTOs;
using PeliculasAPI.DTOs.MoviesActorsDTOs;
using PeliculasAPI.DTOs.MoviesDTOs;
using PeliculasAPI.Model.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using PeliculasAPI.DTOs.UsersDTOs;

namespace PeliculasAPI.Mapper
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles(GeometryFactory geometryFactory)
        {
            #region Gender
            CreateMap<Gender, GenderDTO>().ReverseMap();
            CreateMap<GenderCreationDTO, Gender>();
            #endregion Gender

            #region Cinema
            CreateMap<Cinema, CinemaDTO>()
                .ForMember(x => x.Latitude, x => x.MapFrom(y => y.Ubication.Y))
                .ForMember(x => x.Longitude, x => x.MapFrom(y => y.Ubication.X));
            
            CreateMap<CinemaCreationDTO, Cinema>()
                .ForMember(x => x.Ubication, x => x.MapFrom(y => geometryFactory.CreatePoint(new Coordinate(y.Longitude, y.Latitude))));

            CreateMap<CinemaDTO, Cinema>()
               .ForMember(x => x.Ubication, x => x.MapFrom(y => geometryFactory.CreatePoint(new Coordinate(y.Longitude, y.Latitude))));
            #endregion Cinema

            #region Actor
            CreateMap<Actor, ActorDTO>().ReverseMap();
            CreateMap<Actor, ActorPatchDTO>().ReverseMap();
            CreateMap<ActorCreationDTO, Actor>()
                .ForMember(x => x.Photo, options => options.Ignore());
            #endregion Actor

            #region Movie
            CreateMap<Movie, MovieDTO>().ReverseMap();
            CreateMap<Movie, MoviePatchDTO>().ReverseMap();
            CreateMap<MovieCreationDTO, Movie>()
                .ForMember(x => x.Poster, options => options.Ignore())
                .ForMember(x => x.MoviesGenders, options => options.MapFrom(this.MapMoviesGenders))
                .ForMember(x => x.MoviesActors, options=> options.MapFrom(this.MapMoviesActors));

            CreateMap<Movie, MovieDetailsDTO>()
                .ForMember(x => x.Actors, options => options.MapFrom(this.MapActorDetails))
                .ForMember(x => x.Genders, options => options.MapFrom(this.MapGenderDetails));
            #endregion Movie

            #region User
            CreateMap<IdentityUser, UserDTO>();
            #endregion User
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
