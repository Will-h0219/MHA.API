using AutoMapper;
using MHA.Core.Contracts.Commands.UserCommands;
using MHA.Core.Contracts.Invokers.UserInvokers;
using MHA.Models.DataTransferObjects;
using MHA.Models.Models;
using MHA.Tools.DataProtect;
using System;

namespace MHA.Core.Invokers.UserInvokers
{
    public class InsertUserInvoker : IInsertUserInvoker
    {
        private readonly IGetUserCommand _getUserCommand;
        private readonly IInsertUserCommand _insertUserCommand;
        private readonly IDataProtect _dataProtect;
        private readonly IMapper _mapper;
        public InsertUserInvoker(IGetUserCommand getUserCommand,
            IInsertUserCommand insertUserCommand,
            IDataProtect dataProtect,
            IMapper mapper)
        {
            _getUserCommand = getUserCommand;
            _insertUserCommand = insertUserCommand;
            _dataProtect = dataProtect;
            _mapper = mapper;
        }
        public void Execute(NewUserDTO newUser)
        {
            var userExists = _getUserCommand.ExecuteByEmail(newUser.Email);

            if (userExists != null)
                throw new Exception("User already exists");

            var user = _mapper.Map<User>(newUser);

            user.Password = _dataProtect.ProtectPassword(user.Password);
            
            _insertUserCommand.Execute(user);
        }
    }
}
