using MHA.Models.DataTransferObjects.SubClasses;
using System.ComponentModel.DataAnnotations;

namespace MHA.Models.DataTransferObjects
{
    public class SearchParametersDTO
    {
        public string UserEmail { get; set; }
        public int Page { get; set; }
        public Filters Filters { get; set; }
    }
}
