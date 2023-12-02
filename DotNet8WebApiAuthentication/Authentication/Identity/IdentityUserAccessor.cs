using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DotNet8WebApiAuthentication.Authentication.Identity
{
    public sealed class IdentityUserAccessor
    {
        private readonly UserManager<ApplicationUser> userManager;

        public IdentityUserAccessor(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<ApplicationUser> GetRequiredUserAsync(HttpContext context)
        {
            System.Security.Claims.ClaimsPrincipal user1 = context.User;
            var user = await userManager.GetUserAsync(user1);

            // if (user is null)
            // {
            //     redirectManager.RedirectToWithStatus("Account/InvalidUser", $"Error: Unable to load user with ID '{userManager.GetUserId(context.User)}'.", context);
            // }

            return user;
        }
    }
}