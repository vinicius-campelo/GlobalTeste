using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace Data.Utils
{
   // Criador da chave assimetrica
    public class SigningConfigurations
    {
        public SecurityKey Key { get; set; }
        public SigningCredentials SigningCredentials { get; set; }

        // não usado RSA
        public SigningConfigurations()
        {
            using (var provider = new RSACryptoServiceProvider(2048))
            {
                Key = new RsaSecurityKey(provider.ExportParameters(true));
            }

            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);
        }


        public class SigningHSConfigurations
        {
            public SecurityKey Key { get; set; }
            public SigningCredentials SigningCredentials { get; set; }

            public SigningHSConfigurations(IConfiguration configuration)
            {
                Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Key"]));

                SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256Signature);

            }
        }
    }
}
