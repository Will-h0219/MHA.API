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
    public class GetFilteredCharactersStrategy : ICharacterStrategy
    {
        public async Task<IEnumerable<Character>> Get(SearchParameters parameters, IMongoCollection<Character> collection)
        {
            var filters = BuildFilters(parameters);
            return await collection.Find(filters)
                             .Skip(parameters.Skips)
                             .Limit(parameters.PageSize)
                             .ToListAsync();
        }
        public int Count(SearchParameters parameters, IMongoCollection<Character> collection)
        {
            var filters = BuildFilters(parameters);
            return (int)collection.Find(filters).CountDocuments();
        }

        private FilterDefinition<Character> BuildFilters(SearchParameters parameters)
        {
            var builder = Builders<Character>.Filter;
            var filter = builder.Eq(x => x.UserId, parameters.UserId);

            if (!string.IsNullOrWhiteSpace(parameters.Filters.Name))
            {
                var nameRegex = new BsonRegularExpression(parameters.Filters.Name, "i");
                var nameFilter = builder.Regex(x => x.Name, nameRegex);
                filter &= nameFilter;
            }
            if (!string.IsNullOrWhiteSpace(parameters.Filters.Alias))
            {
                var aliasRegex = new BsonRegularExpression(parameters.Filters.Alias, "i");
                var aliasFilter = builder.Eq(x => x.Alias, aliasRegex);
                filter &= aliasFilter;
            }
            if (!string.IsNullOrWhiteSpace(parameters.Filters.Quirk))
            {
                var quirkRegex = new BsonRegularExpression(parameters.Filters.Quirk, "i");
                var quirkFilter = builder.Regex(x => x.Quirk, quirkRegex);
                filter &= quirkFilter;
            }
            if (!string.IsNullOrWhiteSpace(parameters.Filters.Occupation))
            {
                var occupationRegex = new BsonRegularExpression(parameters.Filters.Occupation, "i");
                var occupationFilter = builder.Eq(x => x.Occupation, occupationRegex);
                filter &= occupationFilter;
            }
            if (!string.IsNullOrWhiteSpace(parameters.Filters.Affiliation))
            {
                var affiliationRegex = new BsonRegularExpression(parameters.Filters.Affiliation, "i");
                var affiliationFilter = builder.Eq(x => x.Affiliation, affiliationRegex);
                filter &= affiliationFilter;
            }

            return filter;
        }
    }
}
