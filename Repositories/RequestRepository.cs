using Chat_Application.Data;
using Chat_Application.Models;
using Microsoft.EntityFrameworkCore;
using Chat_Application.Interface;
namespace Chat_Application.Repositories
{

    public class RequestRepository : IRequestRepository
    {

        private readonly ApplicationDbContext _context;
        public RequestRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<FriendRequest> GetRequestByID(Guid RequestID)
        {
            return await _context.FriendRequests.FindAsync(RequestID);
        }

        public async Task AddRequest(FriendRequest request)
        {
            await _context.FriendRequests.AddAsync(request);
            await _context.SaveChangesAsync();
            
        }
    public async Task UpdateRequest(FriendRequest request)
        {
            _context.FriendRequests.Update(request);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<FriendRequest>> GetAllFriends()
        {
            return await _context.FriendRequests.Where(users=>users.Status =="accepted").ToListAsync();
        }

        public async Task<IEnumerable<FriendRequest>> PendingFriends()
        {
            return await _context.FriendRequests
                .Where(r => r.Status == "pending")
                .ToListAsync(); 
        }

    }

}