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

        public async Task<AuthResponseDTO> Execute(UserCredentialsDTO userCredentials)
        {
            var response = new AuthResponseDTO();

            var user = await _getUserCommand.ExecuteByEmail(userCredentials.Email);

            if (user == null)
            {
                response.Error = 404;
                response.Errors = new List<string>
                {
                    $"There's no user with {userCredentials.Email} mail"
                };
                return response;
            }

            user.Password = _dataProtect.DecryptPassword(user.Password);

            if (user.Password != userCredentials.Password)
            {
                response.Error = 999;
                response.Errors = new List<string>
                {
                    "Password is incorrect."
                };
                return response;
            }

            response.Token = _jwtHandler.CreateToken(userCredentials.Email);

            return response;
        }
    }
}
