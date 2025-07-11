using Chat_Application.Models;
using Chat_Application.Repositories;
using Microsoft.AspNetCore.Mvc;
namespace Chat_Application.Services
{
    public class RequestService : IRequestService
    {
        private readonly IRequestRepository _requestRepository;

        public RequestService(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }

        public async Task<FriendRequest> GetRequestByID(Guid RequestID)
        {
            return await _requestRepository.GetRequestByID(RequestID);
        }

        public async Task AddRequest(FriendRequest request)
        {
            await _requestRepository.AddRequest(request);
        }

        public async Task AcceptRequest(FriendRequest request)
        {
            request.Status = "accepted";
            await _requestRepository.UpdateRequest(request);
        }

        public async Task<IEnumerable<FriendRequest>> GetAllFriends()
        {
            return await _requestRepository.GetAllFriends();
        }

        public async Task<IEnumerable<FriendRequest>> PendingFriends()
        {
            return await _requestRepository.PendingFriends();
        }

        public async Task RejectRequest(FriendRequest request)
        {
            request.Status = "rejected";
            await _requestRepository.UpdateRequest(request);
        }
    }


}