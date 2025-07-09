using Chat_Application.Data;
using Chat_Application.Models;
using Microsoft.EntityFrameworkCore;
    

namespace Chat_Application.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public async Task<List<User>> FindUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> FindUserByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> FindUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Email == email);
        }


        public async Task AddUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateUser(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> FindUserByUsernameAsync(string username)
        {
            return await _context.Users.Where(user => user.Username == username).ToListAsync();
        }


       
    }


    


}
