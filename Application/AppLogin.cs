using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Core.Interfaces.Services;

namespace Application
{

    public class AppLogin : IAppLogin
    {
        private readonly ITokenService _serviceLogin;
        private readonly IMapper _mapper;

        public AppLogin(ITokenService serviceLogin, IMapper mapperCliente)
        {
            _serviceLogin = serviceLogin;
            _mapper = mapperCliente;
        }

        public List<(UserDto?, string)>? Login(string userName, string password)
        {
            var user = _serviceLogin.GetUser(userName, password);


            if (user == null)
                return null;

            var token = _serviceLogin.GenerateToken(user);


            var userMapped = _mapper.Map<UserDto>(user);

            var retorno = new List<(UserDto?, string)>
            {
               ( userMapped,
                token)
            };
            return retorno;
        }
    }
}
