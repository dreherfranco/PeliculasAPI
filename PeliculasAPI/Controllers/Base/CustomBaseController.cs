using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.DTOs.PaginationDTOs;
using PeliculasAPI.FilesManager.Interface;
using PeliculasAPI.Helpers;
using PeliculasAPI.Model.DbConfiguration;
using PeliculasAPI.Model.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasAPI.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CustomBaseController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        protected async Task<ActionResult<List<TDTO>>> Get<TEntity, TDTO>() where TEntity: class
        {
            try
            {
                var entity = await this.context.Set<TEntity>().ToListAsync();
                return mapper.Map<List<TDTO>>(entity);
            }
            catch
            {
                return BadRequest();
            }
        }

        protected async Task<ActionResult<List<TDTO>>> Get<TEntity,TDTO>([FromQuery] PaginationDTO paginationDto) where TEntity: class,IEntity
        {
            try
            {
                var queryable = context.Set<TEntity>().AsQueryable();
                await HttpContext.InsertParametersPagination(queryable, paginationDto.RecordsPerPage);

                var entity = await queryable.Paginate(paginationDto).ToListAsync();

                return this.mapper.Map<List<TDTO>>(entity);
            }

            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        protected async Task<ActionResult<TDTO>> Get<TEntity, TDTO>(int id) where TEntity: class, IEntity
        {
            try
            {
                var entity = await this.context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);
                if (entity == null)
                {
                    return NotFound();
                }
                return mapper.Map<TDTO>(entity);
            }
            catch
            {
                return NotFound();
            }
        }

        protected async Task<ActionResult> Post<TEntity, TCreationDTO, TReadingDTO>([FromBody] TCreationDTO creationDTO, string routeName) where TEntity: class,IEntity
        {
            try
            {
                TryValidateModel(creationDTO);
                var entity = this.mapper.Map<TEntity>(creationDTO);
                this.context.Add(entity);
                await this.context.SaveChangesAsync();
                var readingDTO = this.mapper.Map<TReadingDTO>(entity);
                return new CreatedAtRouteResult(routeName, new { Id = entity.Id }, readingDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        protected async Task<ActionResult> Put<TEntity, TUpdateDTO>(int id, [FromBody] TUpdateDTO updateDTO) where TEntity: class,IEntity
        {
            try
            {
                TryValidateModel(updateDTO);
                var entity = this.mapper.Map<TEntity>(updateDTO);
                entity.Id = id;
                this.context.Entry(entity).State = EntityState.Modified;
                await this.context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        public async Task<ActionResult> Patch<TEntity,TDTO>(int id, [FromBody] JsonPatchDocument<TDTO> patchDocument)
               where TEntity:class, IEntity
               where TDTO: class
        {
            try
            {
                if (patchDocument == null)
                {
                    return BadRequest();
                }

                var entity = await this.context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);

                if (entity == null)
                {
                    return NotFound();
                }

                var Tdto = this.mapper.Map<TDTO>(entity);
                patchDocument.ApplyTo(Tdto, ModelState);

                if (!TryValidateModel(Tdto))
                {
                    return BadRequest(ModelState);
                }

                this.mapper.Map(Tdto, entity);
                await this.context.SaveChangesAsync();

                return NoContent();
            }catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        protected async Task<ActionResult> Delete<TEntity>(int id) where TEntity : class, IEntity, new()
        {
            try
            {
                var entity = this.context.Set<TEntity>().AnyAsync(x => x.Id == id);

                if (entity == null)
                {
                    return NotFound();
                }

                this.context.Remove(new TEntity() { Id = id });
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
