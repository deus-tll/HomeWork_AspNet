using InternetShop.Models.DataModels;
using Microsoft.AspNetCore.Identity;

namespace InternetShop.Models.InitializeModels
{
    public class UserInitializer
    {
        private const string ROLE_MANAGER = "Manager";
        private const string ROLE_ADMIN = "Admin";
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
            await CreateRoleAsync(ROLE_MANAGER);
            await CreateRoleAsync(ROLE_ADMIN);
            await CreateRoleAsync(ROLE_USER);

            await SeedManagementUsersAsync("Managers", ROLE_MANAGER);
            await SeedManagementUsersAsync("Admins", ROLE_ADMIN);
        }


        private async Task CreateRoleAsync(string role)
        {
            var roleExists = await _roleManager.RoleExistsAsync(role);

            if (!roleExists)
                await _roleManager.CreateAsync(new IdentityRole(role));
        }


        private async Task SeedManagementUsersAsync(string section, string role)
        {
            List<ManagementUserConfig>? managerConfigs = _configuration.GetSection(section).Get<List<ManagementUserConfig>>();

            if (managerConfigs is null) return;

            foreach (ManagementUserConfig managerConfig in managerConfigs)
            {
                var manager = await _userManager.FindByEmailAsync(managerConfig.Email);

                if (manager != null) continue;

                manager = new IdentityUser
                {
                    UserName = managerConfig.Email,
                    Email = managerConfig.Email
                };
                await _userManager.CreateAsync(manager, managerConfig.Password);
                await _userManager.AddToRoleAsync(manager, role);
            }
        }
    }
}
