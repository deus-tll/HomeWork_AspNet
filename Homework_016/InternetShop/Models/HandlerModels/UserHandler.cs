using InternetShop.Controllers;
using InternetShop.Models.DataModels;
using InternetShop.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace InternetShop.Models.HandlerModels
{
    public interface IUserHandler
    {
        Task<IdentityResult> SignUpUserAsync(string email, string password, int yearOfBirth, string role, bool signInAfterRegistration = true);
        Task<SignInResult> LoginAsync(string email, string password, bool rememberMe);
        Task LogoutAsync();
        Task DeleteUserAsync(string? userName);
        Task<List<Message>> GetMessages();
        Task<string?> GetUserEmailByIdAsync(string userId);
        Task<string> SendMessageToManager(ClaimsPrincipal currentUser, string messageText);

    }

    public class UserHandler : IUserHandler
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationContext _context;
        private readonly ILogger<UserHandler> _logger;


        public UserHandler(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationContext context, ILogger<UserHandler> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _logger = logger;
        }


        public async Task<List<Message>> GetMessages() => await _context.Messages.ToListAsync();


        public async Task<string> SendMessageToManager(ClaimsPrincipal currentUser, string messageText)
        {
            var user = await _userManager.GetUserAsync(currentUser);
            if (user == null) return "Error";

            var message = new Message
            {
                Text = messageText,
                CreatedAt = DateTime.Now,
                UserId = user.Id
            };

            try
            {
                await _context.Messages.AddAsync(message);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while performing \"Add element to Messages\" operation in the DB.");
            }

            return "Index";
        }


        public async Task<IdentityResult> SignUpUserAsync(string email, string password, int yearOfBirth, string role, bool signInAfterRegistration = true)
        {
            var user = new ApplicationUser { UserName = email, Email = email, YearOfBirth = yearOfBirth };
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, role);

                var claimResult = await _userManager.AddClaimAsync(user, new Claim("YearOfBirth", yearOfBirth.ToString()));

                if (!claimResult.Succeeded)
                {
                    foreach (var error in claimResult.Errors)
                    {
                        _logger.LogError("Error adding claim \"{ClaimType}\" to user: {ErrorDescription}", "YearOfBirth", error.Description);
                    }
                }

                if (signInAfterRegistration)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                }
            }

            return result;
        }


        public async Task<SignInResult> LoginAsync(string email, string password, bool rememberMe)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, rememberMe, lockoutOnFailure: false);
            return result;
        }


        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }


        public async Task DeleteUserAsync(string? userName)
        {
            if (userName is null) return;

            var user = await _userManager.FindByNameAsync(userName);

            if (user != null)
            {
                await _signInManager.SignOutAsync();
                await _userManager.DeleteAsync(user);
            }
        }


        public async Task<string?> GetUserEmailByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user?.Email;
        }
    }
}
