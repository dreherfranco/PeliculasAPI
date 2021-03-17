using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeliculasAPI.Controllers;
using PeliculasAPI.DTOs.GendersDTOs;
using PeliculasAPI.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PeliculasAPI.Tests.UnitTest
{
    [TestClass]
    public class GendersControllerTests: BaseTest
    {
        [TestMethod]
        public async Task GetAllGenders()
        {
            //Preparation
            var nameDb = Guid.NewGuid().ToString();
            var context = this.BuildContext(nameDb);
            var mapper = this.ConfigurationAutoMapper();

            context.Genders.Add(new Model.Models.Gender() { Name = "gender 1" });
            context.Genders.Add(new Model.Models.Gender() { Name = "gender 2" });
            await context.SaveChangesAsync();
            
            var context2 = BuildContext(nameDb);
            //Test
            var controller = new GendersController(context2, mapper);
            var response = await controller.Get();

            //Verification
            var genders = response.Value;
            Assert.AreEqual(2, genders.Count);
        }

        [TestMethod]
        public async Task GetGenderByIdError()
        {
            var nameDb = Guid.NewGuid().ToString();
            var context = this.BuildContext(nameDb);
            var mapper = this.ConfigurationAutoMapper();

            var controller = new GendersController(context, mapper);
            var idTest = 1;
            var response = await controller.Get(idTest);

            var result = response.Result as StatusCodeResult;
            Assert.AreEqual(404,result.StatusCode);
        }

        [TestMethod]
        public async Task GetGenderById()
        {
            //Preparation
            var nameDb = Guid.NewGuid().ToString();
            var context = this.BuildContext(nameDb);
            var mapper = this.ConfigurationAutoMapper();

            context.Genders.Add(new Model.Models.Gender() { Name = "gender 1" });
            await context.SaveChangesAsync();

            var context2 = BuildContext(nameDb);
            //Test
            var controller = new GendersController(context2, mapper);
            var idTest = 1;
            var response = await controller.Get(idTest);

            //Verification
            var gender = response.Value;
            Assert.AreEqual(1, gender.Id);
            Assert.IsNotNull(gender);
        }
    }
}
