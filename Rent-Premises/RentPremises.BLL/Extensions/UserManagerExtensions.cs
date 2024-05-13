using Microsoft.AspNetCore.Identity;
using Rent_Premises.DAL.Entities.Base;

namespace RentPremises.BLL.Extensions
{
    // UserManagerExtensions class contains extension methods for UserManager<User>
    public static class UserManagerExtensions
    {
        // Extension method to set user role asynchronously
        public static async Task<IdentityResult> SetUserRoleAsync(this UserManager<User> userManager, User user, string role)
        {
            // Retrieve roles associated with the user
            var roles = await userManager.GetRolesAsync(user);
            
            // Check if the user already has the specified role
            if (roles.Contains(role))
                return IdentityResult.Success;
            
            // If the user has other roles, remove them
            var removeResult = await userManager.RemoveFromRolesAsync(user, roles);
            if (!removeResult.Succeeded)
                return removeResult;
            
            // Add the specified role to the user
            return await userManager.AddToRoleAsync(user, role);
        }

        // Extension method to get the role of a user asynchronously
        public static async Task<string> GetRoleAsync(this UserManager<User> userManager, User user)
        {
            // Retrieve roles associated with the user
            var roles = await userManager.GetRolesAsync(user);
            
            // Check if the user has exactly one role
            if (roles.Count == 1)
                return roles.First();
            
            // Throw an exception if the user has more than one role
            throw new Exception("User has more than one role");
        }
    }
}