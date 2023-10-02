using Library.Models.ContextModels;
using System.Security.Claims;

namespace Library.Interfaces
{
    public interface IUsersHandler
    {
        Task<ApplicationUser?> GetUserAsync(ClaimsPrincipal user);
    }
}
