using MHA.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHA.Core.Contracts.Commands.CharacterCommands
{
    public interface IUpdateCharacterCommand
    {
        Task Execute(Character character, string userId);
    }
}
