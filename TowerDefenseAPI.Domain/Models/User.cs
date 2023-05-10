namespace TowerDefenseAPI.Domain.Models
{
    public class User
    {
        public string Id { get; set; } = string.Empty;
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public int Score { get; set; }
    }

    public class UserViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string Login { get; set; } = string.Empty;
        public int Score { get; set; }
    }
}
