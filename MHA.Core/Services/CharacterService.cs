using MHA.Core.Contracts.Commands.CharacterCommands;
using MHA.Core.Contracts.Invokers.CharacterInvokers;
using MHA.Core.Contracts.Services;
using MHA.Data.Contracts;
using MHA.Models.DataTransferObjects;
using MHA.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHA.Core.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly IGetCharacterInvoker _getCharacterInvoker;
        private readonly IInsertCharacterInvoker _insertCharacterInvoker;
        private readonly IUpdateCharacterInvoker _updateCharacterInvoker;
        private readonly IDeleteCharacterInvoker _deleteCharacterInvoker;

        public CharacterService(IGetCharacterInvoker getCharacterInvoker, 
            IInsertCharacterInvoker insertCharacterInvoker,            
            IUpdateCharacterInvoker updateCharacterInvoker,
            IDeleteCharacterInvoker deleteCharacterInvoker)
        {
            _getCharacterInvoker = getCharacterInvoker;
            _insertCharacterInvoker = insertCharacterInvoker;
            _updateCharacterInvoker = updateCharacterInvoker;
            _deleteCharacterInvoker = deleteCharacterInvoker;
        }
        public async Task<ResponseDTO> GetResponse(SearchParametersDTO parameters)
        {
            return await _getCharacterInvoker.Execute(parameters);
        }

        public async Task<NewCharacterDTO> InsertCharacter(NewCharacterDTO newCharacter)
        {
            return await _insertCharacterInvoker.Execute(newCharacter);
        }

        public async Task<CharacterDTO> UpdateCharacter(CharacterDTO character, string email)
        {
            return await _updateCharacterInvoker.Execute(character, email);
        }

        public async Task<string> DeleteCharacter(string userEmail, string characterId)
        {
            return await _deleteCharacterInvoker.Execute(userEmail, characterId);
        }
    }
}
