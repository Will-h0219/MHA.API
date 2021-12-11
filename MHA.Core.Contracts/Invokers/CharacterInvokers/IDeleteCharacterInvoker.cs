using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHA.Core.Contracts.Invokers.CharacterInvokers
{
    public interface IDeleteCharacterInvoker
    {
        Task<string> Execute(string userEmail, string characterId);
    }
}
