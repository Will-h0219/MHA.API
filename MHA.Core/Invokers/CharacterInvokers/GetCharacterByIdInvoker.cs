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
    public class GetCharacterByIdInvoker : IGetCharacterByIdInvoker
    {
        private readonly IGetUserCommand _getUserCommand;
        private readonly IGetCharactersCommand _getCharactersCommand;
        private readonly IMapper _mapper;
        public GetCharacterByIdInvoker(IGetUserCommand getUserCommand,
            IGetCharactersCommand getCharactersCommand,
            IMapper mapper)
        {
            _getUserCommand = getUserCommand;
            _getCharactersCommand = getCharactersCommand;
            _mapper = mapper;
        }

        public async Task<CharacterDTO> Execute(string userEmail, string characterId)
        {
            var user = await _getUserCommand.ExecuteByEmail(userEmail);
            var parameters = new SearchParameters()
            {
                UserId = user.Id.ToString(),
                CharacterId = characterId
            };
            var response = await _getCharactersCommand.Execute(parameters);

            if (!response.Any()) return null;

            var responseDTO = _mapper.Map<CharacterDTO[]>(response);

            return responseDTO.ToList()[0];
        }
    }
}
