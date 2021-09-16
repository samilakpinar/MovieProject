using Business.Abstract;
using Microsoft.AspNetCore.DataProtection;

namespace Business.Concrete
{
    public class SecurityManager : ICipherService
    {
        private readonly IDataProtectionProvider _dataProtectorProvider;

        private const string Key = "cut-the-night-with-the-light";

        public SecurityManager(IDataProtectionProvider dataProtectionProvider)
        {
            _dataProtectorProvider = dataProtectionProvider;
        }

        public string Encrypt(string cipherText)
        {
            var protector = _dataProtectorProvider.CreateProtector(Key);
            return protector.Protect(cipherText);
        }

        public string Decrypt(string cipherText)
        {
            var protector = _dataProtectorProvider.CreateProtector(Key);
            return protector.Unprotect(cipherText);
        }


    }
}
