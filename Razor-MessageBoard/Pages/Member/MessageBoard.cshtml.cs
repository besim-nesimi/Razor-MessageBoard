using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor_MessageBoard.Data;
using Razor_MessageBoard.Models;
using Razor_MessageBoard.Services;

namespace Razor_MessageBoard.Pages.Member
{
    public class MessageBoardModel : PageModel
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly MessagesRepo msgRepo;

        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? Message { get; set; }

        public List<MessageModel> Messages { get; set; } = new();
        public MessageBoardModel(SignInManager<IdentityUser> signInManager, MessagesRepo msgRepo)
        {
            this.signInManager = signInManager;
            this.msgRepo = msgRepo;
        }
            public async Task OnGet()
            {
                IdentityUser? user = await signInManager.UserManager.GetUserAsync(HttpContext.User);

                if (user != null)
                {
                    Username = user.UserName;
                }

                Messages = await msgRepo.GetMessagesAsync();
            }

        [HttpPost("sign-out")]
        public async Task<IActionResult> OnPostSignOutAsync()
        {
            await signInManager.SignOutAsync();

            return RedirectToPage("/Index");

        }

        [HttpPost("submit-message")]
        public async Task<IActionResult> OnPostSubmitMessageAsync(MessageModel messageModel)
        {
            // Save the message to the database
            await msgRepo.SaveMessageAsync(messageModel);

            // Refresh the page to display the new message
            return RedirectToPage();
        }
    }
}
