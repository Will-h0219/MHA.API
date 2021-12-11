using MHA.Models.DataTransferObjects;
using MHA.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHA.Core.Contracts.Services
{
    public interface ICharacterService
    {
        Task<ResponseDTO> GetResponse(SearchParametersDTO parameters);
        Task<NewCharacterDTO> InsertCharacter(NewCharacterDTO newCharacter);
        Task<CharacterDTO> UpdateCharacter(CharacterDTO character, string email);
        Task<string> DeleteCharacter(string userEmail, string characterId);
    }
}
