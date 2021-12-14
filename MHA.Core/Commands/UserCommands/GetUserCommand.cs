using MHA.Core.Contracts.Commands.UserCommands;
using MHA.Data.Contracts;
using MHA.Models.DataTransferObjects;
using MHA.Models.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHA.Core.Commands.UserCommands
{
    public class GetUserCommand : IGetUserCommand
    {
        private readonly IClientInstancer _clientInstancer;
        private readonly IMongoCollection<User> _users;
        public GetUserCommand(IClientInstancer clientInstancer)
        {
            _clientInstancer = clientInstancer;
            _users = _clientInstancer.GetCollection<User>();
        }

        public async Task<User> ExecuteByEmail(string email)
        {
            return await _users.Find(x =>x.Email == email)
                         .FirstOrDefaultAsync();
        }
    }
}
