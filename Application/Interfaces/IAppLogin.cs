using Application.Dto;

namespace Application.Interfaces
{
    public interface IAppLogin
    {
        List<(UserDto?, string)>? Login(string Username, string Password);
    }
}
