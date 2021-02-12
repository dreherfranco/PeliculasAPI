﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.DTOs.ActorsDTOs;
using PeliculasAPI.Model.DbConfiguration;
using PeliculasAPI.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PeliculasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ActorsController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
   
        [HttpGet]
        public async Task<ActionResult<List<ActorDTO>>> Get()
        {
            try
            {
                var actorDB = await this.context.Actors.ToListAsync();
                if(actorDB == null)
                {
                    return NotFound();
                }
                return this.mapper.Map<List<ActorDTO>>(actorDB);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpGet("{id}", Name = "GetActor")]
        public async Task<ActionResult<ActorDTO>> Get(int id)
        {
            var actorDB = await this.context.Actors.FirstOrDefaultAsync(x => x.Id == id);
            
            if(actorDB == null)
            {
                return NotFound();
            }

            return this.mapper.Map<ActorDTO>(actorDB);
        }

        
        [HttpPost]
        public async Task<ActionResult<ActorDTO>> Post([FromForm] ActorCreationDTO actorCreationDto)
        {
            TryValidateModel(actorCreationDto);
            var actorDb = this.mapper.Map<Actor>(actorCreationDto);
            this.context.Actors.Add(actorDb);
            await this.context.SaveChangesAsync();
            var actorDto = this.mapper.Map<ActorDTO>(actorDb);

            return new CreatedAtRouteResult("GetActor", new { Id = actorDto.Id }, actorDto);
        }

        
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromForm] ActorUpdateDTO actorDto)
        {
            try
            {
                TryValidateModel(actorDto);
                var actorDb = this.mapper.Map<Actor>(actorDto);
                actorDb.Id = id;
                this.context.Entry(actorDb).State = EntityState.Modified;
                await this.context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var actor = this.context.Actors.AnyAsync(x => x.Id == id);

                if (actor == null)
                {
                    return NotFound();
                }

                this.context.Remove(new Actor() { Id = id });
                await this.context.SaveChangesAsync();
                return NoContent();

            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
