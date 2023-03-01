using DataAccess.Enum;
using System.ComponentModel.DataAnnotations;
#nullable disable

namespace Business.Models
{
    public class TeacherContactModel
    {
        public Gender Gender { get; set; }

        [Required]
        [StringLength(250)]
        public string Email { get; set; }

        [Required]
        [StringLength(1000)]
        public string Address { get; set; }
    }
}
