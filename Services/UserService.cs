using Chat_Application.Models;
using Chat_Application.Repositories;
using System.Runtime.InteropServices;

namespace Chat_Application.Services
{
    public class UserService : IUserService
    {


        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<User>> FindUsersAsync()
        {
            return await _repository.FindUsersAsync();
        }

        public async Task<User> FindUserByIdAsync(Guid userId)
        {
            return await _repository.FindUserByIdAsync(userId);
        }
        public async Task<User> FindUserByEmailAsync(string email)
        {
            return await _repository.FindUserByEmailAsync(email);
        }
        public async Task AddUserAsync(User user)
        {
            await _repository.AddUser(user);
        }
        public async Task DeleteUserAsync(Guid id)
        {
            await _repository.DeleteUser(id);
        }
        public async Task UpdateUserAsync(User user)
        {
            await _repository.UpdateUser(user);
        }
        public async Task<bool> UserExistsAsync(Guid userId)
        {
            return await _repository.FindUserByIdAsync(userId) != null;
        }

        public async Task<bool> UserExistsByEmailAsync(string email)
        {
            return await _repository.FindUserByEmailAsync(email) != null;
        }

        public Task<bool> UserExistsByUsernameAsync(string username)
        {
            return _repository.FindUserByUsernameAsync(username) != null
                ? Task.FromResult(true)
                : Task.FromResult(false);
        }

        public Task<IEnumerable<User>> FindUsersAsyncPagination(int page = 0)
        {
            int limit = 10;
            var offset = page * limit;
            List<User> users = _repository.FindUsersAsync().Result.ToList();

            List<User> Result = new List<User>();

            for (int i = offset; i < (offset + limit); i++)
            {
                Result.Add(users[i]);
            }
            return Task.FromResult<IEnumerable<User>>(Result);
        }

        public async Task<IEnumerable<User>> FindUserByUsernameAsync(string username)
        {
            return (IEnumerable<User>)await _repository.FindUserByUsernameAsync(username);


        }
    }

}