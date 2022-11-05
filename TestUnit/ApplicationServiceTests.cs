using Application;
using Application.Dto;
using AutoFixture;
using AutoMapper;
using Domain.Core.Interfaces.Services;
using Domain.Entities;
using Moq;

namespace TestUnit
{
    public class Tests
    {

        private static Fixture _fixture;
        private Mock<ITokenService> _serviceUserMock;
        private Mock<IMapper> _mapperMock;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _serviceUserMock = new Mock<ITokenService>();
            _mapperMock = new Mock<IMapper>();
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

        [Test]
        public void TestLogin()
        {
            const int id = 10;

            var user = _fixture.Build<User>()
            .With(c => c.Id, id)
            .With(c => c.Username, _userRegistered.Username)
            .With(c => c.Password, _userRegistered.Password)
            .With(c => c.Role, "manager")
            .Create();

            var userDto = _fixture.Build<UserDto>()
                .With(c => c.Id, id)
            .With(c => c.Username, _userRegistered.Username)
            .With(c => c.Password, _userRegistered.Password)
            .With(c => c.Role, "manager")
            .Create();

            _serviceUserMock.Setup(x => x.GetUser(_userRegistered.Username, _userRegistered.Password)).Returns(user);
            _mapperMock.Setup(x => x.Map<UserDto>(user)).Returns(userDto);

            var AppService =
                new AppLogin(_serviceUserMock.Object, _mapperMock.Object);

            var result = AppService.Login(_userRegistered.Username, _userRegistered.Password);

            Assert.IsNotNull(result);
            _serviceUserMock.VerifyAll();
            _mapperMock.VerifyAll();

        }

        [Test]
        public void TestLoginFailure()
        {
            const int id = 10;

            var user = _fixture.Build<User>()
            .With(c => c.Id, id)
            .With(c => c.Username, _userRegistered.Username)
            .With(c => c.Password, _userRegistered.Password)
            .With(c => c.Role, "manager")
            .Create();

            var userDto = _fixture.Build<UserDto>()
                .With(c => c.Id, id)
            .With(c => c.Username, _userRegistered.Username)
            .With(c => c.Password, _userRegistered.Password)
            .With(c => c.Role, "manager")
            .Create();

            _serviceUserMock.Setup(x => x.GetUser("notMatch", _userRegistered.Password)).Returns(user);
            _mapperMock.Setup(x => x.Map<UserDto>(user)).Returns(userDto);

            var AppService =
                new AppLogin(_serviceUserMock.Object, _mapperMock.Object);

            var result = AppService.Login("notMatch", _userNotRegistered.Password);

            Assert.Null(result);

        }
    }
}