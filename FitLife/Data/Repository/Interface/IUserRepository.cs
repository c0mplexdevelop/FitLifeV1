using FitLife.Models.User;

namespace FitLife.Data.Repository.Interface;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(int id);
    Task AddAsync(User user);
    Task AddRangeAsync(IEnumerable<User> users);
    void UpdateAsync(User user);
    void DeleteAsync(User user);
    Task<int> SaveChangesAsync();

}
