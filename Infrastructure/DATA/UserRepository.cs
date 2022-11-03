using Domain.Core.Interfaces.Repositories;
using Domain.Entities;

namespace Infrastructure.DATA
{
    public class UserRepository : IUserRepository
    {
        public User? GetUser(string username, string password)
        {
            var users = new List<User>();
            users.Add(new User { Id = 1, Username = "example@gmail.com", Password = "example", Role = "manager" });
            users.Add(new User { Id = 2, Username = "robin", Password = "robin", Role = "employee" });
            return users.FirstOrDefault(x => x.Username.ToLower() == username.ToLower() && x.Password == password);
        }
    }
}
