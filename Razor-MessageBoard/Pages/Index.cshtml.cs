using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Razor_MessageBoard.Pages
{
    public class IndexModel : PageModel
    {

        private readonly SignInManager<IdentityUser> signInManager;

        [BindProperty]
        [Required]
        public string Username { get; set; }
        [BindProperty]
        [Required]
        public string Password { get; set; }
        public IndexModel(SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        public void OnGet()
        {

        }

        // Logga in
        public async Task<IActionResult> OnPost()
        {

            if (ModelState.IsValid)
            {
                var signInResult = await signInManager.PasswordSignInAsync(Username, Password, false, false);

                if (signInResult.Succeeded)
                {
                    return RedirectToPage("/Member/MessageBoard");
                }
            }

            return Page();
        }
    }
}