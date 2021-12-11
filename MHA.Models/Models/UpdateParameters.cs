using MHA.Models.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHA.Models.Models
{
    public class UpdateParameters
    {
        public string Email { get; set; }
        public NewCharacterDTO Character { get; set; }
    }
}
