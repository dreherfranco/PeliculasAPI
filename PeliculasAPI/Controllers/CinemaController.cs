using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using PeliculasAPI.Controllers.Base;
using PeliculasAPI.DTOs.CinemasDTO;
using PeliculasAPI.Model.DbConfiguration;
using PeliculasAPI.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CinemaController : CustomBaseController
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly GeometryFactory geometryFactory;
        public CinemaController(ApplicationDbContext context, IMapper mapper, GeometryFactory geometryFactory) : base(context,mapper)
        {
            this.context = context;
            this.mapper = mapper;
            this.geometryFactory = geometryFactory;
        }

        [HttpGet]
        public async Task<ActionResult<List<CinemaDTO>>> Get()
        {
            return await Get<Cinema, CinemaDTO>();
        }

        [HttpGet("{id:int}", Name = "GetCinema")]
        public async Task<ActionResult<CinemaDTO>> Get(int id)
        {
            return await Get<Cinema, CinemaDTO>(id);
        }

        [HttpGet("nearby-cinema")]
        public async Task<ActionResult<List<NearbyCinemaDTO>>> GetNearbyCinema([FromQuery] NearbyCinemaFilterDTO filter)
        {
            var userUbication = this.geometryFactory.CreatePoint(new Coordinate(filter.Longitude,filter.Latitude));

            var distanceInMts = filter.DistanceInKms * 1000;
            var cinemas = await this.context.Cinema
                .OrderBy(x => x.Ubication.Distance(userUbication))
                .Where(x => x.Ubication.IsWithinDistance(userUbication, distanceInMts))
                .Select(x => new NearbyCinemaDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Latitude = x.Ubication.Y,
                    Longitude = x.Ubication.X,
                    DistanceInMts = Math.Round(x.Ubication.Distance(userUbication))
                })
                .ToListAsync() ;
            return cinemas;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CinemaCreationDTO cinemaCreationDTO)
        {
            return await Post<Cinema, CinemaCreationDTO, CinemaDTO>(cinemaCreationDTO, "GetCinema");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] CinemaUpdateDTO cinemaUpdateDTO)
        {
            return await Put<Cinema, CinemaUpdateDTO>(id, cinemaUpdateDTO);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await Delete<Cinema>(id);
        }
    }
}
