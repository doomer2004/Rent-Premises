using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Rent_Premises.DAL.Entities.Base;
using RentPremises.Common.Models.Configs;
using RentPremises.Common.Models.DTOs.Auth;

namespace RentPremises.BLL.Services.Auth.Base
{
    public class AuthBase<T> where T : class
    {
        private readonly JwtConfig _jwtConfig;
        protected readonly UserManager<User> _userManager;
        
        // Constructor to initialize JwtConfig and UserManager
        protected AuthBase(JwtConfig jwtConfig, UserManager<User> userManager)
        {
            _jwtConfig = jwtConfig;
            _userManager = userManager;
        }
        
        // Method to generate AccessToken and RefreshToken for a user
        protected async Task<AuthSuccessDto> GenerateAuthSuccessDTO(User user)
        {
            try
            {
                var refreshToken = await GenerateRefreshTokenAsync(user);
                return new AuthSuccessDto
                {
                    AccessToken = await GenerateJwtToken(user),
                    RefreshToken = refreshToken
                };
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }

        // Method to generate a JWT token for a user
        private async Task<string> GenerateJwtToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new []
                {
                    new Claim("id", user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                }),
                Expires = DateTime.UtcNow.Add(_jwtConfig.RefreshTokenLifetime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
                Issuer = _jwtConfig.Issuer,
                Audience = _jwtConfig.Audience
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);
            return await Task.FromResult(jwtToken);
        }
        
        // Method to generate a random RefreshToken
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        
        // Method to generate a RefreshToken for a user and update its properties
        public async Task<string> GenerateRefreshTokenAsync(User user)
        {
            user.RefreshToken = GenerateRefreshToken();
            user.RefreshTokenExpiryTime = DateTimeOffset.UtcNow.Add(_jwtConfig.RefreshTokenLifetime);
            var userUpdated = await _userManager.UpdateAsync(user);
            if (!userUpdated.Succeeded)
            {
                throw new Exception("Unable to refresh token");
            }
            
            return user.RefreshToken;
        }
    }
}
