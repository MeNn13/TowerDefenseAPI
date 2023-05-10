using Microsoft.EntityFrameworkCore;
using TowerDefenseAPI.Data.Interfaces;
using TowerDefenseAPI.Domain.Models;

namespace TowerDefenseAPI.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context) => _context = context;

        public async Task<bool> Create(User entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(User entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User> Get(string id)
        {
#pragma warning disable CS8603 // Возможно, возврат ссылки, допускающей значение NULL.
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<User>> GetAll()
        {
            return _context.Users.ToListAsync();
        }

        public async Task<User> GetLogin(string login)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Login == login);
        }

        public async Task<User> Update(User entity)
        {
            _context.Users.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
