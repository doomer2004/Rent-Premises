using Microsoft.AspNetCore.Identity;

namespace Rent_Premises.Extensions
{
    // HostExtensions class contains extension methods for IHost interface
    public static class HostExtensions
    {
        // Extension method to setup roles asynchronously
        public static async Task SetupRolesAsync(this IHost host)
        {
            // Creating a scope for service resolution
            using var scope = host.Services.CreateScope();
            
            // Obtaining RoleManager service from the service provider
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
            
            // List of roles to be created
            var roles = new List<string> {"Lessor", "Renter" };
            
            // Iterating over each role
            foreach (var role in roles)
            {
                // Checking if the role already exists
                if (!await roleManager.RoleExistsAsync(role))
                {
                    // If the role does not exist, create it
                    await roleManager.CreateAsync(new IdentityRole<Guid>(role));
                }
            }
        }
    }
}