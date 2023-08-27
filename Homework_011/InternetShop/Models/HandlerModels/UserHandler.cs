using InternetShop.Models.DataModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace InternetShop.Models.HandlerModels
{
    public class UserHandler
    {
        private readonly UserManager<IdentityUser>? _userManager;
        private readonly SignInManager<IdentityUser>? _signInManager;
        private readonly ApplicationContext? _context;


        public UserHandler(UserManager<IdentityUser> userManager, ApplicationContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public UserHandler(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }
        public UserHandler(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public UserHandler(ApplicationContext context)
        {
            _context = context;
        }


        public async Task<List<Message>> GetMessages()
        {
            if (_context is null)
                return new();
            else
                return await _context.Messages.ToListAsync();
        }


        public async Task<string?> GetUserEmail(string userId)
        {
            if (_userManager == null) return null;
            var user = await _userManager.FindByIdAsync(userId);
            return user?.Email;
        }


        public async Task<string> SendMessageToManager(ClaimsPrincipal currentUser, string messageText)
        {
            if (_userManager == null) return "/Error";
            var user = await _userManager.GetUserAsync(currentUser);
            if (user == null) return "/Error";

            var message = new Message
            {
                Text = messageText,
                CreatedAt = DateTime.Now,
                UserId = user.Id
            };

            if (_context is null)
                return "/Error";

            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();

            return "/Index";
        }
    }
}
