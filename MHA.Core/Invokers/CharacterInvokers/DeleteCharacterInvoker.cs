using MHA.Core.Contracts.Commands.CharacterCommands;
using MHA.Core.Contracts.Commands.UserCommands;
using MHA.Core.Contracts.Invokers.CharacterInvokers;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHA.Core.Invokers.CharacterInvokers
{
    public class DeleteCharacterInvoker : IDeleteCharacterInvoker
    {
        private readonly IGetUserCommand _getUserCommand;
        private readonly IDeleteCharacterCommand _deleteCharacterCommand;
        public DeleteCharacterInvoker(IGetUserCommand getUserCommand,
            IDeleteCharacterCommand deleteCharacterCommand)
        {
            _getUserCommand = getUserCommand;
            _deleteCharacterCommand = deleteCharacterCommand;
        }

        public async Task<string> Execute(string userEmail, string characterId)
        {
            var user = _getUserCommand.ExecuteByEmail(userEmail);

            var userId = user.Id.ToString();

            var id = ObjectId.Parse(characterId);

            return await _deleteCharacterCommand.Execute(userId, id);
        }
    }
}
