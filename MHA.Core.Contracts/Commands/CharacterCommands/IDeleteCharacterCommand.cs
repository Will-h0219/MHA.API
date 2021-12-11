using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHA.Core.Contracts.Commands.CharacterCommands
{
    public interface IDeleteCharacterCommand
    {
        Task<string> Execute(string userId, ObjectId characterId);
    }
}
