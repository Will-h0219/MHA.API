using MHA.Core.Contracts.Invokers.UserInvokers;
using MHA.Core.Contracts.Services;
using MHA.Models.DataTransferObjects;
using MHA.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHA.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IInsertUserInvoker _insertUserInvoker;
        private readonly IAuthenticateUserInvoker _authenticateUserInvoker;
        public UserService(IInsertUserInvoker insertUserInvoker,
            IAuthenticateUserInvoker authenticateUserInvoker)
        {
            _insertUserInvoker = insertUserInvoker;
            _authenticateUserInvoker = authenticateUserInvoker;
        }

        public void InsertUser(NewUserDTO newUser)
        {
            _insertUserInvoker.Execute(newUser);
        }

        public string AuthenticateUser(UserCredentialsDTO userCredentials)
        {
            return _authenticateUserInvoker.Execute(userCredentials);
        }
    }
}
