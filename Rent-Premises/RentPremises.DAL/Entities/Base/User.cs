using Microsoft.AspNetCore.Identity;

namespace Rent_Premises.DAL.Entities.Base;

public class User : IdentityUser<Guid>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTimeOffset  RefreshTokenExpiryTime { get; set; } 
}