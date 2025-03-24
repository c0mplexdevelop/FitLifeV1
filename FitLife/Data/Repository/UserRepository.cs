using FitLife.Data.Repository.Interface;
using FitLife.Models.User;
using Microsoft.EntityFrameworkCore;

namespace FitLife.Data.Repository;

public class UserRepository : IUserRepository
{
    private readonly DatabaseContext _context;
    public UserRepository(DatabaseContext context)
    {
        _context = context;
    }
    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task AddRangeAsync(IEnumerable<User> users)
    {
        await _context.Users.AddRangeAsync(users);
    }

    public void DeleteAsync(User user)
    {
        _context.Users.Remove(user);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void UpdateAsync(User user)
    {
        _context.Users.Update(user);
    }
}
