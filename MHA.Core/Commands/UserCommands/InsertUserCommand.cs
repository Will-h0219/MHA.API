using MHA.Core.Contracts.Commands.UserCommands;
using MHA.Data.Contracts;
using MHA.Models.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHA.Core.Commands.UserCommands
{
    public class InsertUserCommand : IInsertUserCommand
    {
        private readonly IClientInstancer _clientInstancer;
        private readonly IMongoCollection<User> _collection;
        public InsertUserCommand(IClientInstancer clientInstancer)
        {
            _clientInstancer = clientInstancer;
            _collection = _clientInstancer.GetCollection<User>();
        }

        public async Task Execute(User user)
        {
            await _collection.InsertOneAsync(user);
        }
    }
}
