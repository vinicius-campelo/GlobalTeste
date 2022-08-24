using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace WebSemBanco.Data.Utils
{
    public class SigningConfigurations
    {
        public SecurityKey Key { get; set; }
        public SigningCredentials SigningCredentials { get; set; }


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

            public SigningHSConfigurations()
            {
                string chave = "TESTEGLOBALTEC123";
                Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chave));

                SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256Signature);

            }
        }
    }
}
