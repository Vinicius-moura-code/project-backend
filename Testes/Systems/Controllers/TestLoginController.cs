using API.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Xunit;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Application.Interfaces;
using Application.Dto;
using System.Net;
using Application;

namespace Testes.Systems.Controllers
{
    public class TestLoginController
    {

        private static UserDto _userRegistered = new UserDto()
        {
            Id = 0,
            Username = "example@gmail.com",
            Password = "example",
            Role = "",
        };
        private static UserDto _userNotRegistered = new UserDto()
        {
            Username = "",
            Password = "",
            Role = "",
            Id = 0
        };


        [Fact]
        public void TestLoginAppUserRegistered()
        {

            var mockUser = new Mock<IAppLogin>();
            var service = new LoginController(mockUser.Object);

            var result = service.Authenticate(_userRegistered) as ObjectResult;
            //var actualValue = result.Value;

            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);

            //mockUser.Object.Login(_userRegistered.Username, _userRegistered.Password).Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void TestLoginAppUserNotRegistered()
        {

            var mockUser = new Mock<IAppLogin>();
            mockUser.Setup(service => service.Login(_userNotRegistered.Username, _userNotRegistered.Password));

            var login = new LoginController(mockUser.Object);

            var result = login.Authenticate(_userNotRegistered);

            result.Should().BeOfType<NotFoundResult>();
        }
    }
}

