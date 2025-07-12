
using Chat_Application.Models;

namespace Chat_Application.Interface
{
    public interface IMemberRepository
    {
        Task<IEnumerable<Guid>> GetConversationID(Guid userid);

        Task<bool> ConversationExists(Guid ConversationID);

        Task<IEnumerable<User>> GetMembersPtoP(Guid userID);

        Task<IEnumerable<Guid>> GetUserID(Guid ConversationID);

        Task<IEnumerable<User>> GetGroupMembers(Guid groupId, Guid userid );
        
    }
}