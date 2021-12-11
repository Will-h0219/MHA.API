using MHA.Models.DataTransferObjects.SubClasses;
using MHA.Models.Models;
using System.Collections.Generic;

namespace MHA.Models.DataTransferObjects
{
    public class ResponseDTO
    {
        public Information Info { get; set; }
        public IEnumerable<CharacterDTO> Result { get; set; }
    }
}
