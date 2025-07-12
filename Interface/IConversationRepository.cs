
using Chat_Application.Models;

namespace Chat_Application.Interface
{
    public interface IConversationRepository
    {
        Task CreateConversation(Conversation conversation);

        Task<Conversation> FindConversation(Guid id);

        Task<bool> ConversationExists(Guid id);

        Task<bool> IsGroup(Guid ConversationID);
    }
}