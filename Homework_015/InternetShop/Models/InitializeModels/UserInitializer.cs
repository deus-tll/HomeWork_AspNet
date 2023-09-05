using InternetShop.Models.DataModels;
using Microsoft.AspNetCore.Identity;

namespace InternetShop.Models.InitializeModels
{
    public class UserInitializer
    {
        private const string ROLE_MANAGER = "Manager";
        private const string ROLE_USER = "User";
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public UserInitializer(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }


        public async Task InitializeAsync()
        {
            await CreateManagerRoleAsync();
            await CreateUserRoleAsync();
            await SeedManagersAsync();
        }


        private async Task CreateManagerRoleAsync()
        {
            var roleExists = await _roleManager.RoleExistsAsync(ROLE_MANAGER);
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole(ROLE_MANAGER));
            }
        }


        private async Task CreateUserRoleAsync()
        {
            var roleExists = await _roleManager.RoleExistsAsync(ROLE_USER);
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole(ROLE_USER));
            }
        }


        private async Task SeedManagersAsync()
        {
            List<ManagerConfig>? managerConfigs = _configuration.GetSection("Managers").Get<List<ManagerConfig>>();

            if (managerConfigs is null) return;

            foreach (ManagerConfig managerConfig in managerConfigs)
            {
                var manager = await _userManager.FindByEmailAsync(managerConfig.Email);

                if (manager != null) continue;

                manager = new IdentityUser
                {
                    UserName = managerConfig.Email,
                    Email = managerConfig.Email
                };
                await _userManager.CreateAsync(manager, managerConfig.Password);
                await _userManager.AddToRoleAsync(manager, ROLE_MANAGER);
            }
        }
    }
}
