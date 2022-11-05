using Application;
using Application.Dto;
using Application.Interfaces;
using Application.Mapper;
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
        private readonly IMapper _mapper;

        public TestAppLogin()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new DtoToModelMappingUser());
                    mc.AddProfile(new  ModelToDtoMappingUser());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
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

            var mockUser = new Mock<ITokenService>();
            mockUser.Setup(service => service.GetUser(_userRegistered.Username, _userRegistered.Password));
            
            var service = new AppLogin(mockUser.Object, _mapper);
            
            var result =  service.Login(_userRegistered.Username, _userRegistered.Password);

            Assert.IsType<(UserDto?, string)>(result);
        }

        [Fact]
        public void TestLoginAppUserNotRegistered()
        {

            var mockUser = new Mock<ITokenService>();
            var service = new AppLogin(mockUser.Object, _mapper);

            var result = service.Login(_userNotRegistered.Username, _userNotRegistered.Password);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
