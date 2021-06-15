using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using PeliculasAPI.Controllers;
using PeliculasAPI.DTOs.CinemasDTO;
using PeliculasAPI.Model.Models;
using PeliculasAPI.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PeliculasAPI.Tests.UnitTest
{
    [TestClass]
    public class CinemasControllerTests: BaseTest
    {

        [TestMethod]
        public async Task GetCinema5KmOrLess()
        {
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            using (var context = LocalDbDatabaseInitializer.GetDbContextLocalDb(false))
            {
                var cinemas = new List<Cinema>()
                {
                    new Cinema{ Name = "Agora", Ubication = geometryFactory.CreatePoint(new Coordinate(-69.9388777, 18.4839233)) },
                    new Cinema{ Name = "Agora 2", Ubication = geometryFactory.CreatePoint(new Coordinate(-69.927275, 18.478026)) }
                };

                context.AddRange(cinemas);
                await context.SaveChangesAsync();
            }

            var filter = new NearbyCinemaFilterDTO()
            {
                DistanceInKms = 5,
                Latitude = 18.481139,
                Longitude = -69.938950
            };

            using (var context = LocalDbDatabaseInitializer.GetDbContextLocalDb(false))
            {
                var mapper = ConfigurationAutoMapper();
                var controller = new CinemasController(context, mapper, geometryFactory);
                var response = await controller.GetNearbyCinema(filter);
                var value = response.Value;
                Assert.AreEqual(2, value.Count);
            }
        }
    }
}
