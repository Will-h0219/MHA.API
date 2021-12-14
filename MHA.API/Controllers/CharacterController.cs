using MHA.Core.Contracts.Services;
using MHA.Models.DataTransferObjects;
using MHA.Models.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MHA.API.Controllers
{
    [ApiController]
    [Route("api/character")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet]
        public async Task<ResponseDTO> Get()
        {
            var parameters = new SearchParametersDTO
            {
                UserEmail = EmailClaim().Value
            };

            return await _characterService.GetResponse(parameters);
        }

        [HttpGet("{id}")]
        public async Task<CharacterDTO> GetById(string id)
        {
            var userEmail = EmailClaim().Value;

            return await _characterService.GetCharacterById(userEmail, id);
        }

        [HttpPost]
        public async Task<ResponseDTO> Search(SearchParametersDTO parameters)
        {            
            parameters.UserEmail = EmailClaim().Value;            
            
            return await _characterService.GetResponse(parameters);
        }

        [HttpPost("create")]
        public async Task<NewCharacterDTO> Post(NewCharacterDTO newCharacter)
        {
            var email = EmailClaim().Value;
            return await _characterService.InsertCharacter(newCharacter, email);
        }

        [HttpPut("update")]
        public async Task<CharacterDTO> Put(CharacterDTO character)
        {
            return await _characterService.UpdateCharacter(character, EmailClaim().Value);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult> Delete(string id)
        {
            var result = await _characterService.DeleteCharacter(EmailClaim().Value, id);

            return Ok($"Character with id: {result} deleted");
        }

        private Claim EmailClaim()
        {
            return HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Email)
                                          .FirstOrDefault();
        }
    }
}
