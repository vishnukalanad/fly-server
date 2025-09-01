using fly_server.Models;

namespace fly_server.Services;

public interface IUserService
{
    public IEnumerable<User> GetUsers();
    public User GetUser(int id);
}