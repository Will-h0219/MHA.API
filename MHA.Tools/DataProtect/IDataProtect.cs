using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHA.Tools.DataProtect
{
    public interface IDataProtect
    {
        string ProtectPassword(string plainPassword);
        string DecryptPassword(string encryptedPassword);
    }
}
