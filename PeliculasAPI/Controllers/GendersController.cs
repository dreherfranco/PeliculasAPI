using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class GendersController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        public GendersController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<GenderDTO>>>Get() 
        {
            try
            {
                var genders = await this.context.Genders.ToListAsync();
                return mapper.Map<List<GenderDTO>>(genders);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id:int}",Name = "GetGender")]
        public async Task<ActionResult<GenderDTO>> Get(int id)
        {
            try
            {
                var gender = await this.context.Genders.FirstOrDefaultAsync(x => x.Id == id);
                if(gender == null)
                {
                    return NotFound();
                }
                return mapper.Map<GenderDTO>(gender);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<GenderDTO>> Post([FromBody]GenderCreationDTO genderCreationDto)
        {
            try
            {
                TryValidateModel(genderCreationDto);
                var genderDb = this.mapper.Map<Gender>(genderCreationDto);
                this.context.Add(genderDb);
                await this.context.SaveChangesAsync();
                var genderDto = this.mapper.Map<GenderDTO>(genderDb);
                return new CreatedAtRouteResult("GetGender", new { Id = genderDto.Id }, genderDto);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] GenderUpdateDTO genderDto)
        {
            try
            {
                TryValidateModel(genderDto);
                var genderDb = this.mapper.Map<Gender>(genderDto);
                genderDb.Id = id;
                this.context.Entry(genderDb).State = EntityState.Modified;
                await this.context.SaveChangesAsync();
                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var gender = this.context.Genders.AnyAsync(x => x.Id == id);

                if (gender == null)
                {
                    return NotFound();
                }

                this.context.Remove(new Gender() { Id = id });
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
