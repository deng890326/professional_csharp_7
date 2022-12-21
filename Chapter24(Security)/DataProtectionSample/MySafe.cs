using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProtectionSample
{
    internal class MySafe
    {
        public MySafe(IDataProtectionProvider provider) =>
            protector = provider.CreateProtector("MySafe.MyProtection.v2");

        public string Encrypt(string input) => protector.Protect(input);
        public string Decrypt(string encrypted) =>protector.Unprotect(encrypted);

        private IDataProtector protector;
    }
}
