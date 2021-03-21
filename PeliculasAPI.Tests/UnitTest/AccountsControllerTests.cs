using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeliculasAPI.Model.Models;
using PeliculasAPI.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PeliculasAPI.Tests.UnitTest
{
    [TestClass]
    public class AccountsControllerTests: BaseTest
    {
        [TestMethod]
        public async Task CreateUser()
        {
            var nameDb = Guid.NewGuid().ToString();
            var accountsControllerHelper = new AccountsControllerHelpers();
            await accountsControllerHelper.CreateUserHelper(nameDb);
            var context2 = BuildContext(nameDb);
            var count = await context2.Users.CountAsync();
            Assert.AreEqual(1, count);
        }

        [TestMethod]
        public async Task UserCannotLogin()
        {
            var nameDb = Guid.NewGuid().ToString();
            var accountsControllerHelper = new AccountsControllerHelpers();
            await accountsControllerHelper.CreateUserHelper(nameDb);

            var controller = accountsControllerHelper.BuildAccountsController(nameDb);
            var user = new User() { Email = "example@hotmail.com", Password = "badPassword" };
            var response = await controller.Login(user);

            Assert.IsNull(response.Value);
            var result = response.Result as BadRequestObjectResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task UserCanLogin()
        {
            var nameDb = Guid.NewGuid().ToString();
            var accountsControllerHelper = new AccountsControllerHelpers();
            await accountsControllerHelper.CreateUserHelper(nameDb);

            var controller = accountsControllerHelper.BuildAccountsController(nameDb);
            var user = new User() { Email = "example@hotmail.com", Password = "Aa123456!" };
            var response = await controller.Login(user);
            Assert.IsNotNull(response.Value);
            Assert.IsNotNull(response.Value.Token);
        }

    }
}
