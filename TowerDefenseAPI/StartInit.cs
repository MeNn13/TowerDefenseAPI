using TowerDefenseAPI.Data.Interfaces;
using TowerDefenseAPI.Data.Repositories;
using TowerDefenseAPI.Service.Implementation;
using TowerDefenseAPI.Service.Interfaces;

namespace TowerDefenseAPI
{
    public static class StartInit
    {
        public static void InitializeRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
        }

        public static void InitializeServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }
    }
}
