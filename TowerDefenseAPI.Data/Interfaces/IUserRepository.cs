using TowerDefenseAPI.Domain.Models;

namespace TowerDefenseAPI.Data.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetLogin(string login);
    }
}
