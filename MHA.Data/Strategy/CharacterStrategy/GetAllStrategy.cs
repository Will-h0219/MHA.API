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
    public class GetAllStrategy : ICharacterStrategy
    {
        public async Task<IEnumerable<Character>> Get(SearchParameters parameters, IMongoCollection<Character> collection)
        {
            return await collection.Find(x => x.UserId == parameters.UserId)
                                   .Skip(parameters.Skips)
                                   .Limit(parameters.PageSize)
                                   .ToListAsync();
        }
        public int Count(SearchParameters parameters, IMongoCollection<Character> collection)
        {
            return (int)collection.Find(x => x.UserId == parameters.UserId).CountDocuments();
        }
    }
}
