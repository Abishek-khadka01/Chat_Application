using Chat_Application.DTOS.Response;
namespace Chat_Application.Interface
{
    public interface IMessageRepository
    {
        Task<IEnumerable<MessageResponseDTO>> GetMessagesofGroup(Guid GroupID, int page);

        Task<IEnumerable<MessageResponseDTO>> GetMessagesPtoP(Guid conversationID, int page);


        Task<IEnumerable<LatestMessageDTO>> GetUsersAndLatestMessage(Guid userID);
    }
}
