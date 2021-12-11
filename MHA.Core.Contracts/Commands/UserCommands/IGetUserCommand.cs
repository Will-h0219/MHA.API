using MHA.Models.DataTransferObjects;
using MHA.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHA.Core.Contracts.Commands.UserCommands
{
    public interface IGetUserCommand
    {
        User ExecuteByEmail(string email);
    }
}
