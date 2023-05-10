using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TowerDefenseAPI
{
    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer";
        public const string AUDIENCE = "TowerDefense";
        const string KEY = "secret_key_for_tower_defense";
        public const int LIFETIME = 1;
        public static SymmetricSecurityKey GetSymmetricSecurityKey() => 
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
