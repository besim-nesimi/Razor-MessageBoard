using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Razor_MessageBoard.Pages
{
    [BindProperties]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> signInManager;

        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }

        [Required]
        [Compare(nameof(Password), ErrorMessage = "Passwords don't match!")]
        public string? VerifiedPassword { get; set; }

        public RegisterModel(SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
        }
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                // Skapa en ny identityUser
                IdentityUser newUser = new();
                newUser.UserName = Username;

                // Skapa en användare i databasen
                var createUserResult = await signInManager.UserManager.CreateAsync(newUser, Password);

                // Om vi har lyckats skapa en ny användare
                if (createUserResult.Succeeded)
                {
                    // Logga in användaren med lösenord (sätt även om den ska förbli inloggad).
                    var loginResult = await signInManager.PasswordSignInAsync(newUser, Password, false, false);


                    // Om vi har lyckats logga in, redirecta till rätt sida.
                    if (loginResult.Succeeded)
                    {
                        return RedirectToPage("/Member/MessageBoard");
                    }
                }
            }

            return Page();
        }
    }
}
