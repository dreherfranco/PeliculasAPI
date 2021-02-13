using AutoMapper;
using PeliculasAPI.DTOs.ActorsDTOs;
using PeliculasAPI.DTOs.GendersDTOs;
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
            CreateMap<ActorCreationDTO, Actor>()
                .ForMember(x => x.Photo, options => options.Ignore());
        } 
    }
}
