
using ArtMarketplace.Data;
using ArtMarketplace.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace data.Repository;
public interface IAuthRepository
{
    Task AddUser(User user);
    Task<User> GetUserByEmail(string email);
}
public class AuthRepository(ArtMarketplaceDbContext dbContext) : IAuthRepository
{
    public async Task AddUser(User user)
    {
        if (dbContext.Users.Any(u => u.Email == user.Email))
            throw new Exception("Email already exists");
        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync();
    }

    public async Task<User> GetUserByEmail(string email)
    {
        var user = await dbContext.Users.SingleOrDefaultAsync(u => u.Email == email);
        if (user == null)
            throw new Exception("Invalid credentials");
        return user;
    }
}
