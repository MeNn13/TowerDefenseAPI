using System.Net;
using TowerDefenseAPI.Data.Interfaces;
using TowerDefenseAPI.Domain.Models;
using TowerDefenseAPI.Domain.Response;
using TowerDefenseAPI.Service.Interfaces;

namespace TowerDefenseAPI.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) => _userRepository = userRepository;

        public async Task<IBaseResponse<bool>> Create(User entity)
        {
            var baseResponse = new BaseResponse<bool>();

            try
            {
                var userLogin = _userRepository.GetLogin(entity.Login);

                if (userLogin.Result == null)
                {
                    var user = new User()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Login = entity.Login,
                        Password = BCrypt.Net.BCrypt.HashPassword(entity.Password),
                        Role = entity.Role,
                        Score = 0
                    };

                    await _userRepository.Create(user);

                    baseResponse.StatusCode = HttpStatusCode.OK;
                    return baseResponse;
                }

                baseResponse.StatusCode = HttpStatusCode.BadRequest;
            }
            catch { baseResponse.StatusCode = HttpStatusCode.InternalServerError; }

            return baseResponse;
        }

        public async Task<IBaseResponse<bool>> Delete(string id)
        {
            var baseResponse = new BaseResponse<bool>();

            try
            {
                var user = await _userRepository.Get(id);

                if (user == null)
                {
                    baseResponse.StatusCode = HttpStatusCode.BadRequest;
                    return baseResponse;
                }

                await _userRepository.Delete(user);

                baseResponse.StatusCode = HttpStatusCode.OK;
                return baseResponse;
            }
            catch
            {
                baseResponse.StatusCode = HttpStatusCode.InternalServerError;
                return baseResponse;
            }

        }

        public async Task<IBaseResponse<User>> Get(string id)
        {
            var baseResponse = new BaseResponse<User>();

            try
            {
                var user = await _userRepository.Get(id);

                if (user == null)
                {
                    baseResponse.StatusCode = HttpStatusCode.NotFound;
                    return baseResponse;
                }

                baseResponse.StatusCode= HttpStatusCode.OK;
                baseResponse.Data = user;
                return baseResponse;
            }
            catch
            {
                baseResponse.StatusCode = HttpStatusCode.InternalServerError;
                return baseResponse;
            }

        }

        public async Task<IBaseResponse<IEnumerable<User>>> GetAll()
        {
            var baseResponse = new BaseResponse<IEnumerable<User>>();

            try
            {
                var users = await _userRepository.GetAll();

                if (users.Count == 0)
                {
                    baseResponse.StatusCode = HttpStatusCode.NotFound;
                    return baseResponse;
                }

                baseResponse.Data = users;
                baseResponse.StatusCode = HttpStatusCode.OK;
                return baseResponse;
            }
            catch
            {
                baseResponse.StatusCode = HttpStatusCode.InternalServerError;
                return baseResponse;
            }
        }

        public async Task<IBaseResponse<IEnumerable<UserViewModel>>> GetLeaderboard()
        {
            List<UserViewModel> userViewModels = new List<UserViewModel>();
            var baseResponse = new BaseResponse<IEnumerable<UserViewModel>>();

            try
            {
                var users = await _userRepository.GetAll();

                if (users.Count == 0)
                {
                    baseResponse.StatusCode = HttpStatusCode.NotFound;
                    return baseResponse;
                }

                for (int i = 0; i < users.Count; i++)
                {
                    var userViewModel = new UserViewModel
                    {
                        Id = users[i].Id,
                        Login = users[i].Login,
                        Score = users[i].Score,
                    };

                    userViewModels.Add(userViewModel);
                }
                baseResponse.StatusCode = HttpStatusCode.OK;
                baseResponse.Data = userViewModels;
                return baseResponse;
            }
            catch
            {
                baseResponse.StatusCode = HttpStatusCode.InternalServerError;
                return baseResponse;
            }
        }

        public async Task<IBaseResponse<User>> GetScore(string login)
        {
            var baseResponse = new BaseResponse<User>();

            try
            {
                var user = await _userRepository.GetLogin(login);

                if (user == null)
                {
                    baseResponse.StatusCode = HttpStatusCode.NotFound;
                    return baseResponse;
                }

                baseResponse.StatusCode = HttpStatusCode.OK;
                baseResponse.Data = user;
                return baseResponse;
            }
            catch 
            {
                baseResponse.StatusCode = HttpStatusCode.InternalServerError;
                return baseResponse;
            }
        }

        public async Task<IBaseResponse<User>> Update(User entity)
        {
            var baseResponse = new BaseResponse<User>();

            try
            {
                var user = await _userRepository.Get(entity.Id);

                if (user == null)
                {
                    baseResponse.StatusCode = HttpStatusCode.NotFound;
                    return baseResponse;
                }

                user.Login = entity.Login;
                user.Password = entity.Password;
                user.Role = entity.Role;

                await _userRepository.Update(user);

                baseResponse.StatusCode = HttpStatusCode.OK;
                return baseResponse;
            }
            catch
            {
                baseResponse.StatusCode = HttpStatusCode.InternalServerError;
                return baseResponse;
            }
        }

        public async Task<IBaseResponse<User>> UpdateScore(string login, int score)
        {
            var baseResponse = new BaseResponse<User>();

            try
            {
                var user = await _userRepository.GetLogin(login);

                if (user == null)
                {
                    baseResponse.StatusCode = HttpStatusCode.NotFound;
                    return baseResponse;
                }

                user.Score = score;

                await _userRepository.Update(user);

                baseResponse.StatusCode = HttpStatusCode.OK;
                baseResponse.Data = user;
                return baseResponse;
            }
            catch
            {
                baseResponse.StatusCode = HttpStatusCode.InternalServerError;
                return baseResponse;
            }
        }
    }
}
