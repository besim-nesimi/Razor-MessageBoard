using Razor_MessageBoard.Data;

namespace Razor_MessageBoard.Services
{ 
    public class MessagesRepo
    {
        private readonly AppDbContext context;
        public MessagesRepo(AppDbContext context)
        {
            this.context = context;
        }

        public async Task GetMessages()
        {
            return await context.Messages.ToList();
        }
    }
}
