using Application;
using Application.Dto;
using Application.Interfaces;
using Application.Mapper;
using AutoFixture;
using AutoMapper;
using Domain.Core.Interfaces.Services;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Testes.Systems.Application
{
    public class TestAppLogin
    {
        private static Fixture _fixture;

        private Mock<IMapper> _mapper;
        private Mock<ITokenService> _tokenService;

        public TestAppLogin()
        {
            _fixture = new Fixture();
            _tokenService = new Mock<ITokenService>();
            _mapper = new Mock<IMapper>();
        }

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
            //_tokenService.Setup(x => x.GetUser(_userRegistered.Username, _userRegistered.Password)).Returns();

            //var mockUser = new Mock<ITokenService>();
            _tokenService.Setup(service => service.GetUser(_userRegistered.Username, _userRegistered.Password));
            
            var service = new AppLogin(_tokenService.Object, _mapper.Object);
            
            var result =  service.Login(_userRegistered.Username, _userRegistered.Password);

            Assert.IsType<(UserDto?, string)>(result);


        }

        [Fact]
        public void TestLoginAppUserNotRegistered()
        {

            var mockUser = new Mock<ITokenService>();
            var service = new AppLogin(mockUser.Object, _mapper.Object);

            var result = service.Login(_userNotRegistered.Username, _userNotRegistered.Password);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
