using InternetShop.Models.DataModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace InternetShop.Models.HandlerModels
{
    public class UserHandler
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationContext _context;


        public UserHandler(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ApplicationContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }


        public async Task<List<Message>> GetMessages() => await _context.Messages.ToListAsync();


        public async Task<string?> GetUserEmail(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user?.Email;
        }


        public async Task<string> SendMessageToManager(ClaimsPrincipal currentUser, string messageText)
        {
            var user = await _userManager.GetUserAsync(currentUser);
            if (user == null) return "Home/Error";

            var message = new Message
            {
                Text = messageText,
                CreatedAt = DateTime.Now,
                UserId = user.Id
            };

            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();

            return "Home/Index";
        }
    }
}
