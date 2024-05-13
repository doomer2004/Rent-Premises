using Microsoft.AspNetCore.Identity;

namespace Rent_Premises.DAL.Entities.Base
{
    // User class inherits from IdentityUser<Guid> provided by ASP.NET Core Identity
    public class User : IdentityUser<Guid>
    {
        // FirstName property represents the first name of the user
        public string FirstName { get; set; } = string.Empty;

        // LastName property represents the last name of the user
        public string LastName { get; set; } = string.Empty;

        // Email property represents the email address of the user
        public string Email { get; set; } = string.Empty;

        // RefreshToken property represents the refresh token associated with the user
        public string RefreshToken { get; set; } = string.Empty;

        // RefreshTokenExpiryTime property represents the expiry time of the refresh token
        public DateTimeOffset RefreshTokenExpiryTime { get; set; }
    }
}