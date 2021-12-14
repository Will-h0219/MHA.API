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
    public class UpdateCharacterInvoker : IUpdateCharacterInvoker
    {
        private readonly IMapper _mapper;
        private readonly IGetUserCommand _getUserCommand;
        private readonly IUpdateCharacterCommand _updateCharacterCommand;
        public UpdateCharacterInvoker(IMapper mapper,
            IGetUserCommand getUserCommand,
            IUpdateCharacterCommand updateCharacterCommand)
        {
            _mapper = mapper;
            _getUserCommand = getUserCommand;
            _updateCharacterCommand = updateCharacterCommand;
        }

        public async Task<CharacterDTO> Execute(CharacterDTO updatedCharacter, string userEmail)
        {
            var user = await _getUserCommand.ExecuteByEmail(userEmail);

            var userId = user.Id.ToString();

            var character = _mapper.Map<Character>(updatedCharacter);

            character.UserId = userId;

            await _updateCharacterCommand.Execute(character, userId);

            return updatedCharacter;
        }
    }
}
