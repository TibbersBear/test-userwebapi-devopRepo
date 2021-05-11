using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using aspnet_core_dotnet_core.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver.Linq;
using Moq;
using TestTechAwsLogin.Controllers;
using TestTechAwsLogin.Models;

namespace aspnet_core_dotnet_core.FunctionalTests
{
    [TestClass]
    public class UserControllerTest
    {
        private UserController _controller;
        private Mock<IUserModelService> _mock;
        private IUserModelService _service;

        [TestInitialize]
        public void TestInit()
        {
            _mock = new Mock<IUserModelService>();
            _service = _mock.Object;
            _controller = new UserController(_service);
        }

        [TestMethod]
        public void Get_WhenCalledOnEmptyDB_ReturnsNoResult()
        {
            // Act
            var notFoundResult = _controller.Get();
            // Assert
            Assert.IsInstanceOfType(notFoundResult.Result, typeof(NotFoundResult));
            Assert.IsNull(notFoundResult.Value);
        }

        [TestMethod]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            //Setup
            SetupDefaultGet();
            // Act
            var okResult = _controller.Get();
            // Assert
            Assert.IsNotNull(okResult.Value, "Result should not be null");
            Assert.AreEqual(okResult.Value.Count, 3, "Unexpected number of results.");
            Assert.AreEqual(okResult.Value.First().Name, _arthur.Name, "Unexpected values in result.");
        }

        private UserModel _arthur = new UserModel() { Id = Guid.NewGuid().ToString(), Name = "Arthur", Fear = "Snakes" };
        private UserModel _thomas = new UserModel() { Id = Guid.NewGuid().ToString(), Name = "Thomas", Fear = "Chicken" };
        private UserModel _george = new UserModel() { Id = Guid.NewGuid().ToString(), Name = "George", Fear = "Dragons" };

        private void SetupDefaultGet()
        {
            _mock.Setup(s => s.Get()).Returns(new List<UserModel>()
            {
                _arthur, _thomas,_george
            });
        }
    }
}
