using Microsoft.EntityFrameworkCore;
using Razor_MessageBoard.Data;
using Razor_MessageBoard.Models;

namespace Razor_MessageBoard.Services
{ 
    public class MessagesRepo
    {
        private readonly AppDbContext context;
        public MessagesRepo(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<List<MessageModel>> GetMessagesAsync()
        {
            return await context.Messages.ToListAsync();
        }

        public async Task SaveMessageAsync(MessageModel messageModel)
        {
            // Create a new Message entity and set its properties from the MessageModel object
            MessageModel message = new()
            {
                Date = messageModel.Date,
                Message = messageModel.Message,
                Username = messageModel.Username
            };

            // Add the new message to the context and save the changes to the database
            context.Messages.Add(message);
            await context.SaveChangesAsync();
        }

    }
}
