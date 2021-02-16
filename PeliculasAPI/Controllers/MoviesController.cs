using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.DTOs.MoviesDTOs;
using PeliculasAPI.DTOs.PaginationDTOs;
using PeliculasAPI.FilesManager.Interface;
using PeliculasAPI.Helpers;
using PeliculasAPI.Model.DbConfiguration;
using PeliculasAPI.Model.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly string container = "actors";
        private readonly IFileManager fileManager;
        public MoviesController(ApplicationDbContext context, IMapper mapper, IFileManager fileManager)
        {
            this.context = context;
            this.mapper = mapper;
            this.fileManager = fileManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<MovieDTO>>> Get([FromQuery] PaginationDTO paginationDto)
        {
            try
            {
                var queryable = context.Movies.AsQueryable();
                await HttpContext.InsertParametersPagination(queryable, paginationDto.RecordsPerPage);

                var movieDB = await queryable.Paginate(paginationDto).ToListAsync();

                return this.mapper.Map<List<MovieDTO>>(movieDB);
            }

            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpGet("{id}", Name = "GetMovie")]
        public async Task<ActionResult<MovieDTO>> Get(int id)
        {
            var movieDB = await this.context.Movies.FirstOrDefaultAsync(x => x.Id == id);

            if (movieDB == null)
            {
                return NotFound();
            }

            return this.mapper.Map<MovieDTO>(movieDB);
        }


        [HttpPost]
        public async Task<ActionResult<MovieDTO>> Post([FromForm] MovieCreationDTO movieCreationDto)
        {
            TryValidateModel(movieCreationDto);
            var movieDb = this.mapper.Map<Movie>(movieCreationDto);

            if (movieCreationDto.Poster != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await movieCreationDto.Poster.CopyToAsync(memoryStream);
                    var content = memoryStream.ToArray();
                    var extension = Path.GetExtension(movieCreationDto.Poster.FileName);
                    movieDb.Poster = await this.fileManager.SaveFile(content, extension, this.container, movieCreationDto.Poster.ContentType);
                }
            }

            this.orderActors(movieDb);
            this.context.Movies.Add(movieDb);
            await this.context.SaveChangesAsync();
            var movieDto = this.mapper.Map<MovieDTO>(movieDb);

            return new CreatedAtRouteResult("GetMovie", new { Id = movieDto.Id }, movieDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromForm] MovieUpdateDTO movieDto)
        {
            try
            {
                TryValidateModel(movieDto);
                var movieDb = await this.context.Movies
                    .Include(x=> x.MoviesActors)
                    .Include(x=>x.MoviesGenders)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (movieDb == null) { return NotFound(); }

                movieDb = this.mapper.Map(movieDto, movieDb);
                if (movieDto.Poster != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await movieDto.Poster.CopyToAsync(memoryStream);
                        var content = memoryStream.ToArray();
                        var extension = Path.GetExtension(movieDto.Poster.FileName);
                        movieDb.Poster = await this.fileManager.EditFile(content, extension, this.container, movieDb.Poster, movieDto.Poster.ContentType);
                    }
                }

                this.orderActors(movieDb);
                movieDb.Id = id;
                this.context.Entry(movieDb).State = EntityState.Modified;
                await this.context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<MoviePatchDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var movieDb = await this.context.Movies.FirstOrDefaultAsync(x => x.Id == id);

            if (movieDb == null)
            {
                return NotFound();
            }

            var movieDto = this.mapper.Map<MoviePatchDTO>(movieDb);
            patchDocument.ApplyTo(movieDto, ModelState);

            if (!TryValidateModel(movieDto))
            {
                return BadRequest(ModelState);
            }

            this.mapper.Map(movieDto, movieDb);
            await this.context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var movie = this.context.Movies.AnyAsync(x => x.Id == id);

                if (movie == null)
                {
                    return NotFound();
                }

                this.context.Remove(new Movie() { Id = id });
                await this.context.SaveChangesAsync();
                return NoContent();

            }
            catch
            {
                return BadRequest();
            }
        }

        private void orderActors(Movie movie)
        {
            if(movie.MoviesActors != null)
            {
                for(int i=0; i < movie.MoviesActors.Count();i++)
                {
                    movie.MoviesActors[i].Order = i;
                }
            }
        } 
    }
}
