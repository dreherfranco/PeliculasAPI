using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PeliculasAPI.Controllers;
using PeliculasAPI.DTOs.PaginationDTOs;
using PeliculasAPI.Model.Models;
using PeliculasAPI.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeliculasAPI.Tests.UnitTest
{
    [TestClass]
    public class MoviesControllerTests: BaseTest
    {
        private string CreateDataTest()
        {
            var nameDb = Guid.NewGuid().ToString();
            var context = BuildContext(nameDb);
            var gender = new Gender() { Name = "genre 1" };

            var movies = new List<Movie>()
            {
                new Movie(){Title = "Movie 1", PremiereDate = new DateTime(2010, 1,1), InTheaters = false},
                new Movie(){Title = "Not released", PremiereDate = DateTime.Today.AddDays(1), InTheaters = false},
                new Movie(){Title = "Movie in theaters", PremiereDate = DateTime.Today.AddDays(-1), InTheaters = true}
            };

            var movieWithGender = new Movie()
            {
                Title = "Movies with Gender",
                PremiereDate = new DateTime(2010, 1, 1),
                InTheaters = false
            };
            movies.Add(movieWithGender);

            context.Add(gender);
            context.AddRange(movies);
            context.SaveChanges();

            var movieGender = new MoviesGenders() { GenderId = gender.Id, MovieId = movieWithGender.Id };
            context.Add(movieGender);
            context.SaveChanges();

            return nameDb;
        }

        [TestMethod]
        public async Task FiltrarPorTitulo()
        {
            var nameDb = CreateDataTest();
            var context = BuildContext(nameDb);
            var mapper = ConfigurationAutoMapper();

            var controller = new MoviesController(context, mapper, null);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();

            var titleMovie = "Movie 1";

            var filterDTO = new MovieFilterDTO()
            {
                Title = titleMovie,
                RecordsPerPage = 10
            };

            var response = await controller.Filter(filterDTO);
            var movies = response.Value;
            Assert.AreEqual(1, movies.Count);
            Assert.AreEqual(titleMovie, movies[0].Title);
        }

        [TestMethod]
        public async Task FilterInCinemas()
        {
            var nameDb = CreateDataTest();
            var context = BuildContext(nameDb);
            var mapper = ConfigurationAutoMapper();

            var controller = new MoviesController(context, mapper, null);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();

            var filterDTO = new MovieFilterDTO()
            {
                InTheaters = true
            };

            var response = await controller.Filter(filterDTO);
            var movies = response.Value;
            Assert.AreEqual(1, movies.Count);
            Assert.AreEqual("Movie in theaters", movies[0].Title);
        }

        [TestMethod]
        public async Task FilterNextReleases()
        {
            var nameDb = CreateDataTest();
            var context = BuildContext(nameDb);
            var mapper = ConfigurationAutoMapper();

            var controller = new MoviesController(context, mapper, null);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();

            var filterDTO = new MovieFilterDTO()
            {
                NextReleases = true
            };

            var response = await controller.Filter(filterDTO);
            var movies = response.Value;
            Assert.AreEqual(1, movies.Count);
            Assert.AreEqual("Not released", movies[0].Title);
        }

        [TestMethod]
        public async Task FilterByGender()
        {
            var nameDb = CreateDataTest();
            var mapper = ConfigurationAutoMapper();
            var context = BuildContext(nameDb);

            var controller = new MoviesController(context, mapper, null);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();

            var genderId = context.Genders.Select(x => x.Id).First();

            var filterDTO = new MovieFilterDTO()
            {
                GenderId = genderId
            };

            var response = await controller.Filter(filterDTO);
            var movies = response.Value;
            Assert.AreEqual(1, movies.Count);
            Assert.AreEqual("Movie with Gender", movies[0].Title);
        }

        [TestMethod]
        public async Task FiltrarOrdenaTituloAscendente()
        {
            var nameDb = CreateDataTest();
            var mapper = ConfigurationAutoMapper();
            var context = BuildContext(nameDb);

            var controller = new MoviesController(context, mapper, null);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();

            var filterDTO = new MovieFilterDTO()
            {
                FieldToSort = "title",
                AscendingOrder = false
            };

            var response = await controller.Filter(filterDTO);
            var movies = response.Value;

            var context2 = BuildContext(nameDb);
            var moviesDb = context2.Movies.OrderBy(x => x.Title).ToList();

            Assert.AreEqual(moviesDb.Count, movies.Count);

            for (int i = 0; i < moviesDb.Count; i++)
            {
                var movieOfController = movies[i];
                var movieDb = moviesDb[i];

                Assert.AreEqual(movieDb.Id, movieOfController.Id);
            }
        }

        [TestMethod]
        public async Task FiltrarTituloDescendente()
        {
            var nameDb = CreateDataTest();
            var mapper = ConfigurationAutoMapper();
            var context = BuildContext(nameDb);

            var controller = new MoviesController(context, mapper, null);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();

            var filterDTO = new MovieFilterDTO()
            {
                FieldToSort = "title",
                AscendingOrder = true
            };

            var response = await controller.Filter(filterDTO);
            var movies = response.Value;

            var contexto2 = BuildContext(nameDb);
            var moviesDb = contexto2.Movies.OrderByDescending(x => x.Title).ToList();

            Assert.AreEqual(moviesDb.Count, movies.Count);

            for (int i = 0; i < moviesDb.Count; i++)
            {
                var movieOfController = movies[i];
                var movieDb = moviesDb[i];

                Assert.AreEqual(movieDb.Id, movieOfController.Id);
            }
        }

        [TestMethod]
        public async Task FiltrarPorCampoIncorrectoDevuelvePeliculas()
        {
            var nameDb = CreateDataTest();
            var mapper = ConfigurationAutoMapper();
            var context = BuildContext(nameDb);

            //var mock = new Mock<ILogger<PeliculasController>>();

            var controller = new MoviesController(context, mapper, null);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();

            var filterDTO = new MovieFilterDTO()
            {
                FieldToSort = "abc",
                AscendingOrder = false
            };

            var response = await controller.Filter(filterDTO);
            var movies = response.Value;

            var contexto2 = BuildContext(nameDb);
            var moviesDb = contexto2.Movies.ToList();
            Assert.AreEqual(moviesDb.Count, movies.Count);
          // Assert.AreEqual(1, Mock.Invocations.Count);
        }
    }
}
