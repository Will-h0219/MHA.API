using MHA.Models.DataTransferObjects;
using MHA.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHA.Core.Contracts.Invokers.UserInvokers
{
    public interface IInsertUserInvoker
    {
        void Execute(NewUserDTO newUser);
    }
}
