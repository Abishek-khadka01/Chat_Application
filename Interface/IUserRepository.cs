using Chat_Application.Models;

namespace Chat_Application.Interface
{
    public interface IUserRepository
    {

        Task<List<User>> FindUsersAsync();

        Task<User> FindUserByIdAsync(Guid userId);

        Task<User> FindUserByEmailAsync(string email);

        Task AddUser(User user);

        Task DeleteUser(Guid id);

        Task UpdateUser(User user);


        Task<IEnumerable<User>> FindUserByUsernameAsync(string username);




    }
}
