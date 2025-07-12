using Chat_Application.Models;

namespace Chat_Application.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<User>> FindUsersAsync();

        Task<User> FindUserByIdAsync(Guid userId);
        Task<User> FindUserByEmailAsync(string email);
        Task AddUserAsync(User user);
        Task DeleteUserAsync(Guid id);
        Task UpdateUserAsync(User user);
        Task<bool> UserExistsAsync(Guid userId);

        Task<bool> UserExistsByEmailAsync(string email);

        Task<bool> UserExistsByUsernameAsync(string username);

        Task<IEnumerable<User>> FindUsersAsyncPagination(int page);

        Task<IEnumerable<User>>
        FindUserByUsernameAsync(string username);
    }
}
        