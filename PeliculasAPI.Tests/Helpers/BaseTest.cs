using Microsoft.EntityFrameworkCore;
using PeliculasAPI.Model.DbConfiguration;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using NetTopologySuite;
using PeliculasAPI.Mapper;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace PeliculasAPI.Tests.Helpers
{
    public class BaseTest
    {
        protected string userDefaultId = "9722b56a-77ea-4e41-941d-e319b6eb3712";
        protected string userDefaultEmail = "example@hotmail.com";

        protected ApplicationDbContext BuildContext(string nameDb)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(nameDb).Options;

            var dbContext = new ApplicationDbContext(options);
            return dbContext;
        }

        protected IMapper ConfigurationAutoMapper()
        {
            var config = new MapperConfiguration(options =>
            {
                var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
                options.AddProfile(new AutoMapperProfiles(geometryFactory));
            });

            return config.CreateMapper();
        }

        protected ControllerContext BuildControllerContext()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, userDefaultEmail),
                new Claim(ClaimTypes.Email, userDefaultEmail),
                new Claim(ClaimTypes.NameIdentifier, userDefaultId)
            }));

            return new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
        }
    }
}
