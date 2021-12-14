using MHA.Models.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHA.Core.Contracts.Invokers.UserInvokers
{
    public interface IAuthenticateUserInvoker
    {
        Task<AuthResponseDTO> Execute(UserCredentialsDTO userCredentials);
    }
}
