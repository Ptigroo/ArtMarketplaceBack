using ArtMarketplace.Controllers.DTOs.Auth;
using ArtMarketplace.Domain.Models;
using data.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ArtMarketplace.Domain.Services;
public interface IAuthService
{
    Task<string> RegisterAsync(RegisterRequest request);
    Task<string> LoginAsync(LoginRequest request);
}
public class AuthService(IAuthRepository authRepository, IPasswordHasher<User> passwordHasher, IConfiguration config) : IAuthService
{
    public async Task<string> RegisterAsync(RegisterRequest request)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            Role = request.Role
        };
        user.PasswordHash = passwordHasher.HashPassword(user, request.Password);

        await authRepository.AddUser(user);
        return GenerateJwtToken(user);
    }

    public async Task<string> LoginAsync(LoginRequest request)
    {
        var user = await authRepository.GetUserByEmail(request.Email);
        var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
        if (result != PasswordVerificationResult.Success)
            throw new Exception("Invalid credentials");
        return GenerateJwtToken(user);
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim("id", user.Id.ToString()),
            new Claim("email", user.Email),
            new Claim("role", user.Role)
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: config["Jwt:Issuer"],
            audience: config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: creds
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
