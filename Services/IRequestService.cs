using Chat_Application.Models;

namespace Chat_Application.Services
{
    public interface IRequestService
    {
        Task<FriendRequest> GetRequestByID(Guid RequestID);
        Task AddRequest(FriendRequest request);
        Task AcceptRequest(FriendRequest request);
        Task RejectRequest(FriendRequest request);
        
        Task<IEnumerable<FriendRequest>> GetAllFriends();
        Task<IEnumerable<FriendRequest>> PendingFriends();
    }
}

