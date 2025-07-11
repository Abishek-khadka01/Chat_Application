using Chat_Application.Data;
using Chat_Application.Models;
using Microsoft.EntityFrameworkCore;
using Chat_Application.Interface;
namespace Chat_Application.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly ApplicationDbContext _context;
       

        private readonly ConversationRepository _conversationRepository;
        public MemberRepository(ApplicationDbContext context, UserRepository repository, ConversationRepository conversation)
        {
            _context = context;
        
            _conversationRepository = conversation;
        }

        public async Task<IEnumerable<Guid>> GetConversationID(Guid userid)
        {
            return await _context.ConversationMembers.Where(convo => convo.UserId == userid).Select(convo => convo.ConversationId).ToListAsync();
        }

        public async Task<bool> ConversationExists(Guid conversationId)
        {
            int memberCount = await _context.ConversationMembers
                .Where(cm => cm.ConversationId == conversationId)
                .CountAsync();

            return memberCount >= 2;
        }


       

                public async Task<IEnumerable<User>> GetMembersPtoP(Guid userId)
            {
                var result = await (
                    from convomembers in _context.ConversationMembers
                    join conversations in _context.Conversations
                        on convomembers.ConversationId equals conversations.Id
                    join user in _context.Users
                        on convomembers.UserId equals user.Id
                    where conversations.IsGroup == false && convomembers.UserId != userId
                    select user
                ).ToListAsync();

                return result;
            

                        /*

select conversation_members.userid from conversation_members join conversation 
on conversation_members.conversationid == conversation.id && conversation.isgroup ==false


*/
        }



        public async Task<IEnumerable<Guid>> GetUserID(Guid ConversationID)
        {
            return await _context.ConversationMembers.Where(convo => convo.ConversationId == ConversationID)
            .Select(convo =>
            convo.UserId).ToListAsync();
        }

        public async Task<IEnumerable<User>> GetGroupMembers(Guid GroupId, Guid userID)
        {
            var result = await (from conversation in _context.Conversations
                                join convomembers in _context.ConversationMembers
                                on conversation.Id equals convomembers.ConversationId
                                join users in _context.Users
                                on convomembers.UserId equals users.Id
                                where conversation.Id == GroupId && conversation.IsGroup == true
                                && convomembers.UserId != userID
                select users 
                
                
                ).ToListAsync();

            return result.Distinct();
       }

    }
}