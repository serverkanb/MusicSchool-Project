using AppCore.Records.Bases;
using System.ComponentModel.DataAnnotations;
#nullable disable

namespace Business.Models
{
    public class ClassroomModel:RecordBase
    {
        [Required(ErrorMessage = "{0} is required!")]
        [MinLength(3, ErrorMessage = "{0} must be minimum {1} characters!")]
        [MaxLength(200, ErrorMessage = "{0} must be maximum {1} characters!")]
        public string Name { get; set; }
    }
}
