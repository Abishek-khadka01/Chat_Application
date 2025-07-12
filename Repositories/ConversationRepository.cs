
using Chat_Application.Interface;
using System.ComponentModel;
using System.Diagnostics;
using Chat_Application.Data;
using Chat_Application.Models;

namespace Chat_Application.Repositories
{
    public class ConversationRepository : IConversationRepository
    {

        private readonly ApplicationDbContext _context;

        public ConversationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateConversation(Conversation conversation)
        {
            await _context.Conversations.AddAsync(conversation);
            await _context.SaveChangesAsync();
        }

        public async Task<Conversation> FindConversation(Guid id)
        {
            return await _context.Conversations.FindAsync(id);
        }

        public async Task<bool> ConversationExists(Guid id)
        {
            return await _context.Conversations.FindAsync(id) != null;
        }

        public async Task<bool> IsGroup(Guid ConversationID)
        {
            var convo = await _context.Conversations.FindAsync(ConversationID);

            return (bool)convo.IsGroup;
        }

    }
}