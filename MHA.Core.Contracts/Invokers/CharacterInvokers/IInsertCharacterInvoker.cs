using MHA.Models.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHA.Core.Contracts.Invokers.CharacterInvokers
{
    public interface IInsertCharacterInvoker
    {
        Task<NewCharacterDTO> Execute(NewCharacterDTO newCharacter, string userEmail);
    }
}
