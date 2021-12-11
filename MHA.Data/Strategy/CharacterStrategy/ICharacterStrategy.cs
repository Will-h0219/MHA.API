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
    public interface ICharacterStrategy
    {
        Task<IEnumerable<Character>> Get(SearchParameters parameters, IMongoCollection<Character> collection);
        int Count(SearchParameters parameters, IMongoCollection<Character> collection);
    }
}
