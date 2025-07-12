
using Chat_Application.Models;

namespace Chat_Application.Interface
{
    public interface IMemberRepository
    {
        Task<IEnumerable<Guid>> GetConversationID(Guid userid);

        Task<bool> ConversationExists(Guid ConversationID);

        Task<IEnumerable<ConversationMember>> GetConversationMembers(Guid conversationID);

        Task<IEnumerable<ConversationMember>> GetMembersPtoP(Guid conversationID);

        Task<IEnumerable<Guid>> GetUserID(Guid ConversationID);
    }
}