using MHA.Core.Contracts.Commands.CharacterCommands;
using MHA.Data.Contracts;
using MHA.Data.Strategy.CharacterStrategy;
using MHA.Models.DataTransferObjects;
using MHA.Models.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHA.Core.Commands.CharacterCommands
{
    public class GetCharactersCommand : IGetCharactersCommand
    {
        private readonly IClientInstancer _clientInstancer;
        private readonly IMongoCollection<Character> _collection;
        public GetCharactersCommand(IClientInstancer clientInstancer)
        {
            _clientInstancer = clientInstancer;
            _collection = _clientInstancer.GetCollection<Character>();
        }

        public async Task<IEnumerable<Character>> Execute(SearchParameters parameters)
        {
            var characterContext = new CharacterContext(new GetAllStrategy());

            if (!string.IsNullOrWhiteSpace(parameters.CharacterId))
            {
                characterContext = new CharacterContext(new GetCharacterByIdStrategy());
            }
            if (parameters.Filters != null)
            {
                var filters = HasFilters(parameters);
                characterContext = filters ?
                    new CharacterContext(new GetFilteredCharactersStrategy()) :
                    characterContext;
            }

            return await characterContext.Get(parameters, _collection);
        }

        public int ExecuteCount(SearchParameters parameters)
        {
            var characterContext = new CharacterContext(new GetAllStrategy());

            if (!string.IsNullOrWhiteSpace(parameters.CharacterId))
            {
                characterContext = new CharacterContext(new GetCharacterByIdStrategy());
            }
            if (parameters.Filters != null)
            {
                var filters = HasFilters(parameters);
                characterContext = filters ?
                    new CharacterContext(new GetFilteredCharactersStrategy()) :
                    characterContext;
            }

            return characterContext.Count(parameters, _collection);

            //return (int)_collection.Find(x => x.UserId == parameters.UserId).CountDocuments();
        }

        private bool HasFilters(SearchParameters parameters)
        {
            var filters = parameters.Filters.GetType().GetProperties()
                                    .Select(pi => (string)pi.GetValue(parameters.Filters))
                                    .Any(value => !string.IsNullOrWhiteSpace(value));
            return filters;
        }
    }
}
