using Microsoft.AspNetCore.Identity;

namespace Rent_Premises.Extensions;

public static class HostExtensions
{
    public static async Task SetupRolesAsync(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
        var roles = new List<string> {"Lessor", "Renter" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(role));
            }
        }
    }
}