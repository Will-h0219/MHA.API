using MHA.Tools.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHA.Models.Models
{
    [CollectionName("users")]
    public class User : ModelBase
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
