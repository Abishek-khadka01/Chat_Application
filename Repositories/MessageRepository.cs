using Chat_Application.Data;
using Chat_Application.DTOS.Response;
using Chat_Application.Interface;
using Chat_Application.Models;
using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Chat_Application.Repositories
{
    public class MessageRepository : IMessageRepository
    {

        private readonly ApplicationDbContext _context;

        public MessageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MessageResponseDTO>>GetMessagesofGroup (Guid GroupID, int start)
        {
            var result = await (from message in _context.Messages
                                join conversation in _context.Conversations
                                on message.ConversationId equals conversation.Id
                                join convo_members in _context.ConversationMembers
                                on conversation.Id equals convo_members.ConversationId
                                join users in _context.Users
                                on convo_members.UserId equals users.Id
                                where conversation.Id == GroupID && conversation.IsGroup == true
                                orderby message.SentAt

                                select new MessageResponseDTO(users.Username, conversation.Name, message.Content, users.Id)



            ).Skip((start - 1) * 20).Take(20).ToListAsync();


            return result;
        }

        public async Task<IEnumerable<MessageResponseDTO>> GetMessagesPtoP(Guid conversationID, int offset)
        {
            var result = await (from message in _context.Messages
                                join conversation in _context.Conversations
                                on message.ConversationId equals conversation.Id
                                join convo_members in _context.ConversationMembers
                                on conversation.Id equals convo_members.ConversationId
                                join users in _context.Users
                                on convo_members.UserId equals users.Id
                                where conversation.Id == conversationID && conversation.IsGroup == false
                                orderby message.SentAt

                                select new MessageResponseDTO(users.Username, conversation.Name, message.Content, users.Id)



               ).Skip((offset - 1) * 20).Take(20).ToListAsync();


            return result;
        }

        public async Task<IEnumerable<LatestMessageDTO>> GetUsersAndLatestMessage(Guid userID)
        {
                var result = await (
                    from convoMember in _context.ConversationMembers
                    where convoMember.UserId == userID

                    join message in _context.Messages
                        on convoMember.ConversationId equals message.ConversationId

                    join conversation in _context.Conversations
                        on message.ConversationId equals conversation.Id

                    join sender in _context.Users
                        on message.SenderId equals sender.Id

                    where message.SenderId != userID

                    group message by message.ConversationId into grouped

                    let latestMessage = grouped
                        .OrderByDescending(m => m.SentAt)
                        .FirstOrDefault()

                    join conversation in _context.Conversations
                        on grouped.Key equals conversation.Id

                    join sender in _context.Users
                        on latestMessage.SenderId equals sender.Id

                    select new LatestMessageDTO
                    {
                        ConversationID = (Guid)grouped.Key,
                        Message = latestMessage.Content,
                        SentAt = (DateTime)latestMessage.SentAt,
                        Name = (bool)conversation.IsGroup 
                            ? conversation.Name 
                            : sender.Username,
                        IsGroup = (bool)conversation.IsGroup,
                        ProfilePicture = sender.ProfilePictureUrl
                    }
                ).ToListAsync();


            return result;
        }
    }
    
}
