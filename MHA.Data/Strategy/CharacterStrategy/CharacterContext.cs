using MHA.Models.DataTransferObjects;
using MHA.Models.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHA.Data.Strategy.CharacterStrategy
{
    public class CharacterContext
    {
        private ICharacterStrategy _strategy;
        public ICharacterStrategy Strategy
        {
            set
            {
                _strategy = value;
            }
        }
        public CharacterContext(ICharacterStrategy strategy)
        {
            _strategy = strategy;
        }

        public async Task<IEnumerable<Character>> Get(SearchParameters parameters, IMongoCollection<Character> collection)
        {
            return await _strategy.Get(parameters, collection);
        }

        public int Count(SearchParameters parameters, IMongoCollection<Character> collection)
        {
            return _strategy.Count(parameters, collection);
        }
    }
}
