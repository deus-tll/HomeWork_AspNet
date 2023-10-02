using Library.Interfaces;
using Library.Models.ContextModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace Library.Models.HandlerModels
{
    public class UsersHandler : IUsersHandler
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UsersHandler> _logger;

        public UsersHandler(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context, ILogger<UsersHandler> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _logger = logger;
        }

        public async Task<ApplicationUser?> GetUserAsync(ClaimsPrincipal user) => await _userManager.GetUserAsync(user);
    }
}
