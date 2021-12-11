using MHA.Core.Contracts.Commands.CharacterCommands;
using MHA.Data.Contracts;
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
    public class DeleteCharacterCommand : IDeleteCharacterCommand
    {
        private readonly IClientInstancer _clientInstancer;
        private readonly IMongoCollection<Character> _collection;
        public DeleteCharacterCommand(IClientInstancer clientInstancer)
        {
            _clientInstancer = clientInstancer;
            _collection = _clientInstancer.GetCollection<Character>();
        }

        public async Task<string> Execute(string userId, ObjectId characterId)
        {
            var builder = Builders<Character>.Filter;

            var filter = builder.Eq(x => x.Id, characterId) & builder.Eq(x => x.UserId, userId);
            
            await _collection.FindOneAndDeleteAsync(filter);

            return characterId.ToString();
        }
    }
}
