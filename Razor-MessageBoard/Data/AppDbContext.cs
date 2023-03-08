using Microsoft.EntityFrameworkCore;
using Razor_MessageBoard.Models;

namespace Razor_MessageBoard.Data
{
        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
            {

            }


        public DbSet<MessageModel> Messages { get; set; }
    }
}
