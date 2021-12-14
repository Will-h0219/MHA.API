using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHA.Models.DataTransferObjects
{
    public class AuthResponseDTO
    {
        public int Error { get; set; } = 000;
        public List<string> Errors { get; set; }
        public string Token { get; set; }
    }
}
