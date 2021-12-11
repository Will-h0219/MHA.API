using MHA.Core.Contracts.Commands.UserCommands;
using MHA.Core.Contracts.Invokers.UserInvokers;
using MHA.Models.DataTransferObjects;
using MHA.Tools.DataProtect;
using MHA.Tools.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MHA.Core.Invokers.UserInvokers
{
    public class AuthenticateUserInvoker : IAuthenticateUserInvoker
    {
        private readonly IDataProtect _dataProtect;
        private readonly IJwtHandler _jwtHandler;
        private readonly IGetUserCommand _getUserCommand;
        public AuthenticateUserInvoker(IDataProtect dataProtect,
            IJwtHandler jwtHandler,
            IGetUserCommand getUserCommand)
        {
            _dataProtect = dataProtect;
            _jwtHandler = jwtHandler;
            _getUserCommand = getUserCommand;
        }

        public string Execute(UserCredentialsDTO userCredentials)
        {
            var user = _getUserCommand.ExecuteByEmail(userCredentials.Email);

            if (user == null) return null;

            user.Password = _dataProtect.DecryptPassword(user.Password);

            if (user.Password != userCredentials.Password) return null;

            return _jwtHandler.CreateToken(userCredentials.Email);
        }
    }
}
