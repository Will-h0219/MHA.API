using AutoMapper;
using MHA.Core.Contracts.Commands.CharacterCommands;
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
        public InsertCharacterInvoker(IMapper mapper,
            IInsertCharacterCommand insertCharacterCommand)
        {
            _mapper = mapper;
            _insertCharacterCommand = insertCharacterCommand;
        }

        public async Task<NewCharacterDTO> Execute(NewCharacterDTO newCharacter)
        {
            var character = _mapper.Map<Character>(newCharacter);
            await _insertCharacterCommand.Execute(character);
            return newCharacter;
        }
    }
}
