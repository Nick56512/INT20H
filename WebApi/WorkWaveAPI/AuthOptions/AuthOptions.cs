using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WorkWaveAPI.ApiConfig
{
    public class AuthOptions
    {
        public const string ISSUER = "WEBHUNTERS";
        public const string AUDIENCE = "INT20T";
        const string KEY = "APISECRETSECURITYKEY";
        public const int LIFETIME = 10000;
        public static SymmetricSecurityKey GetSymetricKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
