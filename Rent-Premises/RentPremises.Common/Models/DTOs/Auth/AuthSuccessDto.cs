namespace RentPremises.Common.Models.DTOs.Auth;

public class AuthSuccessDto
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}