﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        public CinemaController(ApplicationDbContext context, IMapper mapper): base(context,mapper)
        {
            this.context = context;
            this.mapper = mapper;
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
