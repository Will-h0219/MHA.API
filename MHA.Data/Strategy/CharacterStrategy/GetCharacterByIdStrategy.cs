using MHA.Models.DataTransferObjects;
using MHA.Models.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHA.Data.Strategy.CharacterStrategy
{
    public class GetCharacterByIdStrategy : ICharacterStrategy
    {
        public async Task<IEnumerable<Character>> Get(SearchParameters parameters, IMongoCollection<Character> collection)
        {
            return await collection.Find(x => x.UserId == parameters.UserId && x.Id == ObjectId.Parse(parameters.CharacterId))
                                   .Skip(parameters.Skips)
                                   .Limit(parameters.PageSize)
                                   .ToListAsync();
        }
        public int Count(SearchParameters parameters, IMongoCollection<Character> collection)
        {
            return (int)collection.Find(x => x.UserId == parameters.UserId && x.Id == ObjectId.Parse(parameters.CharacterId)).CountDocuments();
        }
    }
}
