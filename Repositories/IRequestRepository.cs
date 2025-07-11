
using Chat_Application.Models;

namespace  Chat_Application.Repositories
{
    public interface IRequestRepository
    {
        Task<FriendRequest> GetRequestByID(Guid RequestID);

        Task AddRequest(FriendRequest request);

        Task UpdateRequest(FriendRequest request);

        Task<IEnumerable<FriendRequest>> GetAllFriends();

        Task<IEnumerable<FriendRequest>> PendingFriends();


    }
}