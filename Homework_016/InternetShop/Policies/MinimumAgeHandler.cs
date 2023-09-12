using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace InternetShop.Policies
{
    public class MinimumAgeRequirement : IAuthorizationRequirement
    {
        public int MinimumAge { get; }

        public MinimumAgeRequirement(int minimumAge)
        {
            MinimumAge = minimumAge;
        }
    }

    public class MinimumAgeHandler : AuthorizationHandler<MinimumAgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
            const string claimName = "YearOfBirth";
            if (context.User.HasClaim(c => c.Type == claimName))
            {
                _ = int.TryParse(context.User.FindFirst(c => c.Type == claimName)?.Value, out int yearOfBirth);
                var currentYear = DateTime.Now.Year;

                if (currentYear - yearOfBirth >= requirement.MinimumAge)
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
