using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.Controllers.Base;
using PeliculasAPI.DTOs.ActorsDTOs;
using PeliculasAPI.DTOs.PaginationDTOs;
using PeliculasAPI.FilesManager;
using PeliculasAPI.FilesManager.Interface;
using PeliculasAPI.Helpers;
using PeliculasAPI.Model.DbConfiguration;
using PeliculasAPI.Model.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PeliculasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : CustomBaseController
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly string container = "actors";
        private readonly IFileManager fileManager;
        public ActorsController(ApplicationDbContext context, IMapper mapper, IFileManager fileManager): base(context,mapper)
        {
            this.context = context;
            this.mapper = mapper;
            this.fileManager = fileManager;
        }
   
        [HttpGet]
        public async Task<ActionResult<List<ActorDTO>>> Get([FromQuery]PaginationDTO paginationDto)
        {
            return await Get<Actor, ActorDTO>(paginationDto);

        }

        [HttpGet("{id}", Name = "GetActor")]
        public async Task<ActionResult<ActorDTO>> Get(int id)
        {
            return await Get<Actor, ActorDTO>(id);
        }

        
        [HttpPost]
        public async Task<ActionResult> Post([FromForm] ActorCreationDTO actorCreationDto)
        {
            //TryValidateModel(actorCreationDto);
            var actorDb = this.mapper.Map<Actor>(actorCreationDto);
           
            if(actorCreationDto.Photo != null)
            {
                using(var memoryStream = new MemoryStream())
                {
                    await actorCreationDto.Photo.CopyToAsync(memoryStream);
                    var content = memoryStream.ToArray();
                    var extension = Path.GetExtension(actorCreationDto.Photo.FileName);
                    actorDb.Photo = await this.fileManager.SaveFile(content, extension, this.container, actorCreationDto.Photo.ContentType);
                }
            }

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
                var actorDb = await this.context.Actors.FirstOrDefaultAsync(x => x.Id == id);

                if(actorDb == null) { return NotFound(); }

                actorDb = this.mapper.Map(actorDto, actorDb);
                if (actorDto.Photo != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await actorDto.Photo.CopyToAsync(memoryStream);
                        var content = memoryStream.ToArray();
                        var extension = Path.GetExtension(actorDto.Photo.FileName);
                        actorDb.Photo = await this.fileManager.EditFile(content, extension, this.container,actorDb.Photo, actorDto.Photo.ContentType);
                    }
                }

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

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<ActorPatchDTO> patchDocument)
        {
            return await Patch<Actor, ActorPatchDTO>(id, patchDocument);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await Delete<Actor>(id);
        }
    }
}
