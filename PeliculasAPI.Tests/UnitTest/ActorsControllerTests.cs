using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PeliculasAPI.Controllers;
using PeliculasAPI.DTOs.ActorsDTOs;
using PeliculasAPI.DTOs.PaginationDTOs;
using PeliculasAPI.FilesManager.Interface;
using PeliculasAPI.Model.Models;
using PeliculasAPI.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PeliculasAPI.Tests.UnitTest
{
    [TestClass]
    public class ActorsControllerTests: BaseTest
    {
        [TestMethod]
        public async Task GetPeoplePaginated()
        {
            var nameDb = Guid.NewGuid().ToString();
            var context = BuildContext(nameDb);
            var mapper = ConfigurationAutoMapper();

            context.Actors.Add(new Actor() { Name = "Actor 1" });
            context.Actors.Add(new Actor() { Name = "Actor 2" });
            context.Actors.Add(new Actor() { Name = "Actor 3" });
            await context.SaveChangesAsync();

            var context2 = BuildContext(nameDb);

            var controller = new ActorsController(context2, mapper, null);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();

            var page1 = await controller.Get(new PaginationDTO() { Page = 1, RecordsPerPage = 2 });
            var actorsPage1 = page1.Value;
            Assert.AreEqual(2, actorsPage1.Count);

            controller.ControllerContext.HttpContext = new DefaultHttpContext();

            var page2 = await controller.Get(new PaginationDTO() { Page = 2, RecordsPerPage = 2 });
            var actorsPage2 = page2.Value;
            Assert.AreEqual(1, actorsPage2.Count);

            controller.ControllerContext.HttpContext = new DefaultHttpContext();

            var page3 = await controller.Get(new PaginationDTO() { Page = 3, RecordsPerPage = 2 });
            var actorsPage3 = page3.Value;
            Assert.AreEqual(0, actorsPage3.Count);
        }

        [TestMethod]
        public async Task CreateActorNoPhoto()
        {
            var nameDb = Guid.NewGuid().ToString();
            var context = BuildContext(nameDb);
            var mapper = ConfigurationAutoMapper();

            var actor = new ActorCreationDTO() { Name = "Felipe", Birthday = DateTime.Now };

            var mock = new Mock<IFileManager>();
            mock.Setup(x => x.SaveFile(null, null, null, null))
                .Returns(Task.FromResult("url"));

            var controller = new ActorsController(context, mapper, mock.Object);
            var response = await controller.Post(actor);
            var result = response as CreatedAtRouteResult;
            Assert.AreEqual(201, result.StatusCode);

            var context2 = BuildContext(nameDb);
            var listed = await context2.Actors.ToListAsync();
            Assert.AreEqual(1, listed.Count);
            Assert.IsNull(listed[0].Photo);

            Assert.AreEqual(0, mock.Invocations.Count);
        }

        [TestMethod]
        public async Task CreateActorWithPhoto()
        {
            var nameDb = Guid.NewGuid().ToString();
            var context = BuildContext(nameDb);
            var mapper = ConfigurationAutoMapper();

            var content = Encoding.UTF8.GetBytes("Test image");
            var file = new FormFile(new MemoryStream(content), 0, content.Length, "Data", "image.jpg");
            file.Headers = new HeaderDictionary();
            file.ContentType = "image/jpg";

            var actor = new ActorCreationDTO()
            {
                Name = "new actor",
                Birthday = DateTime.Now,
                Photo = file
            };

            var mockFileManager = new Mock<IFileManager>();
            mockFileManager.Setup(x => x.SaveFile(content, ".jpg", "actors", file.ContentType))
                .Returns(Task.FromResult("url"));

            var controller = new ActorsController(context, mapper, mockFileManager.Object);
            var response = await controller.Post(actor);
            var result = response as CreatedAtRouteResult;
            Assert.AreEqual(201, result.StatusCode);

            var context2 = BuildContext(nameDb);
            var listed = await context2.Actors.ToListAsync();
            Assert.AreEqual(1, listed.Count);
            Assert.AreEqual("url", listed[0].Photo);
            Assert.AreEqual(1, mockFileManager.Invocations.Count);
        }

        [TestMethod]
        public async Task PatchReturn404IfActorNotExist()
        {
            var nameDb = Guid.NewGuid().ToString();
            var context = BuildContext(nameDb);
            var mapper = ConfigurationAutoMapper();

            var controller = new ActorsController(context, mapper, null);
            var patchDoc = new JsonPatchDocument<ActorPatchDTO>();
            var response = await controller.Patch(1, patchDoc);
            var result = response as StatusCodeResult;
            Assert.AreEqual(404, result.StatusCode);
        }

        [TestMethod]
        public async Task PatchUpdateJustOneField()
        {
            var nameDb = Guid.NewGuid().ToString();
            var context = BuildContext(nameDb);
            var mapper = ConfigurationAutoMapper();

            var birthday = DateTime.Now;
            var actor = new Actor() { Name = "Franco", Birthday = birthday };
            context.Add(actor);
            await context.SaveChangesAsync();

            var context2 = BuildContext(nameDb);
            var controller = new ActorsController(context2, mapper, null);

            var objectValidator = new Mock<IObjectModelValidator>();
            objectValidator.Setup(x => x.Validate(It.IsAny<ActionContext>(),
                It.IsAny<ValidationStateDictionary>(),
                It.IsAny<string>(),
                It.IsAny<object>()));

            controller.ObjectValidator = objectValidator.Object;

            var patchDoc = new JsonPatchDocument<ActorPatchDTO>();
            patchDoc.Operations.Add(new Operation<ActorPatchDTO>("replace", "/name", null, "Agustin"));
            var response = await controller.Patch(1, patchDoc);
            var result = response as StatusCodeResult;
            Assert.AreEqual(204, result.StatusCode);

            var context3 = BuildContext(nameDb);
            var actorDB = await context3.Actors.FirstAsync();
            Assert.AreEqual("Agustin", actorDB.Name);
            Assert.AreEqual(birthday, actorDB.Birthday);
        }
    }
}
