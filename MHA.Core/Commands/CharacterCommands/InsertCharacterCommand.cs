using MHA.Core.Contracts.Commands.CharacterCommands;
using MHA.Data.Contracts;
using MHA.Models.DataTransferObjects;
using MHA.Models.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHA.Core.Commands.CharacterCommands
{
    public class InsertCharacterCommand : IInsertCharacterCommand
    {
        private readonly IClientInstancer _clientInstancer;
        private readonly IMongoCollection<Character> _collection;
        public InsertCharacterCommand(IClientInstancer clientInstancer)
        {
            _clientInstancer = clientInstancer;
            _collection = _clientInstancer.GetCollection<Character>();
        }

        public async Task Execute(Character character)
        {
            await _collection.InsertOneAsync(character);
        }
    }
}
