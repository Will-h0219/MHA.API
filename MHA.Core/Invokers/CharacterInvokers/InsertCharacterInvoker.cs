using AutoMapper;
using MHA.Core.Contracts.Commands.CharacterCommands;
using MHA.Core.Contracts.Commands.UserCommands;
using MHA.Core.Contracts.Invokers.CharacterInvokers;
using MHA.Models.DataTransferObjects;
using MHA.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHA.Core.Invokers.CharacterInvokers
{
    public class InsertCharacterInvoker : IInsertCharacterInvoker
    {
        private readonly IMapper _mapper;
        private readonly IInsertCharacterCommand _insertCharacterCommand;
        private readonly IGetUserCommand _getUserCommand;
        public InsertCharacterInvoker(IMapper mapper,
            IInsertCharacterCommand insertCharacterCommand,
            IGetUserCommand getUserCommand)
        {
            _mapper = mapper;
            _insertCharacterCommand = insertCharacterCommand;
            _getUserCommand = getUserCommand;
        }

        public async Task<NewCharacterDTO> Execute(NewCharacterDTO newCharacter, string userEmail)
        {
            var user = _getUserCommand.ExecuteByEmail(userEmail);

            var character = _mapper.Map<Character>(newCharacter);
            character.UserId = user.Id.ToString();

            await _insertCharacterCommand.Execute(character);
            return newCharacter;
        }
    }
}
