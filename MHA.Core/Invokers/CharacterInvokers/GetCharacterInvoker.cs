using AutoMapper;
using MHA.Core.Contracts.Commands.CharacterCommands;
using MHA.Core.Contracts.Commands.UserCommands;
using MHA.Core.Contracts.Invokers.CharacterInvokers;
using MHA.Models.DataTransferObjects;
using MHA.Models.DataTransferObjects.SubClasses;
using MHA.Models.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHA.Core.Invokers.CharacterInvokers
{
    public class GetCharacterInvoker : IGetCharacterInvoker
    {
        private readonly IMapper _mapper;
        private readonly IGetCharactersCommand _getCharacterCommand;
        private readonly IGetUserCommand _getUserCommand;
        public GetCharacterInvoker(IGetCharactersCommand getCharacterCommand,
            IGetUserCommand getUserCommand,
            IMapper mapper)
        {
            _getCharacterCommand = getCharacterCommand;
            _getUserCommand = getUserCommand;
            _mapper = mapper;
        }

        public async Task<ResponseDTO> Execute(SearchParametersDTO parametersDTO)
        {
            var response = new ResponseDTO();
            var info = new Information();
            var currentPage = parametersDTO.Page == 0 ? 1 : parametersDTO.Page;

            var user = _getUserCommand.ExecuteByEmail(parametersDTO.UserEmail);

            if (parametersDTO.Page > 0)
                parametersDTO.Page = (parametersDTO.Page - 1) * 20;

            var parameters = _mapper.Map<SearchParameters>(parametersDTO);
            parameters.UserId = user.Id.ToString();

            info.CurrentPage = currentPage;
            info.Count = _getCharacterCommand.ExecuteCount(parameters);
            info.Pages = (int)Math.Ceiling((double)info.Count / 20);

            var result = await _getCharacterCommand.Execute(parameters);
            var resultDTO = _mapper.Map<IEnumerable<CharacterDTO>>(result);

            response.Info = info;
            response.Result = resultDTO;

            return response;
        }
    }
}
