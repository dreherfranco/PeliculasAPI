using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasAPI.Helpers
{
    public static class HttpContextExtensions
    {
        public async static Task InsertParametersPagination<T>(this HttpContext httpContext,
                                                               IQueryable<T> queryable,
                                                               int recordsPerPage)
        {
            double quantity = await queryable.CountAsync();
            double quantityPages = Math.Ceiling(quantity / recordsPerPage);
            httpContext.Response.Headers.Add("quantityPages", quantityPages.ToString());
        }
    }
}
