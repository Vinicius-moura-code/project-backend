using Domain.Entities;

namespace Domain.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        User? GetUser(string username, string password);
    }
}
