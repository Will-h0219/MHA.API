using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHA.Tools.DataProtect
{
    public class DataProtect : IDataProtect
    {
        private readonly IConfiguration _configuration;
        private readonly IDataProtector _dataProtector;
        private readonly string _protectKey;
        public DataProtect(IConfiguration configuration,
            IDataProtectionProvider dataProtectionProvider)
        {
            _configuration = configuration;
            _protectKey = _configuration.GetSection("MyMongoSettings").GetSection("pwdProtector").Value;
            _dataProtector = dataProtectionProvider.CreateProtector(_protectKey);
        }

        public string ProtectPassword(string plainPassword)
        {
            return _dataProtector.Protect(plainPassword);
        }

        public string DecryptPassword(string encryptedPassword)
        {
            return _dataProtector.Unprotect(encryptedPassword);
        }
    }
}
