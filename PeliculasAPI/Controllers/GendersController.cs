using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.Controllers.Base;
using PeliculasAPI.DTOs.GendersDTOs;
using PeliculasAPI.Model.DbConfiguration;
using PeliculasAPI.Model.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PeliculasAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GendersController : CustomBaseController
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        public GendersController(ApplicationDbContext context, IMapper mapper):base(context,mapper)
        {
            
        }

        [HttpGet]
        public async Task<ActionResult<List<GenderDTO>>>Get() 
        {
            return await Get<Gender, GenderDTO>();
        }

        [HttpGet("{id:int}",Name = "GetGender")]
        public async Task<ActionResult<GenderDTO>> Get(int id)
        {
            return await Get<Gender, GenderDTO>(id);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]GenderCreationDTO genderCreationDto)
        {
            return await Post<Gender, GenderCreationDTO, GenderDTO>(genderCreationDto, "GetGender" );
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] GenderUpdateDTO genderDto)
        {
            return await Put<Gender, GenderUpdateDTO>(id, genderDto);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await Delete<Gender>(id);
        }
    }
}
