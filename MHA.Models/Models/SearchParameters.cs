using MHA.Models.DataTransferObjects.SubClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHA.Models.Models
{
    public class SearchParameters
    {
        public string UserId { get; set; }
        public int PageSize { get; set; } = 20;
        public int Skips { get; set; }
        public string CharacterId { get; set; }
        public Filters Filters { get; set; }
    }
}
