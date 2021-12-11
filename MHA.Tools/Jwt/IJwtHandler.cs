using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MHA.Tools.Jwt
{
    public interface IJwtHandler
    {
        string CreateToken(string email);
        string RenewToken(IEnumerable<Claim> claims);
    }
}
