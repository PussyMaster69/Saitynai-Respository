using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Project
{
    public class AuthenticationOptions
    {
        public static string Audience { get; } = "MatasAudience";
        public static string Issuer { get; } = "Matas";
        public static RsaSecurityKey Key { get; } = new RsaSecurityKey(GenerateRSAKey());
        public static SigningCredentials SigningCredentials { get; } = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);

        public static TimeSpan ExpiresSpan { get; } = TimeSpan.FromMinutes(60);
        public static string TokenType { get; } = "Bearer";

        private static RSAParameters GenerateRSAKey()
        {
            using (var key = new RSACryptoServiceProvider(2048))
            {
                return key.ExportParameters(true);
            }
        }

    }

   
}
