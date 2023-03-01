using DataAccess.Enum;
using System.ComponentModel.DataAnnotations;
#nullable disable //sor
namespace DataAccess.Entities
{
    public class TeacherContact
    {
        [Key]
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public Gender Gender { get; set; }

        [Required]
        [StringLength(250)]
        public string Email { get; set; }

        [Required]
        [StringLength(1000)]
        public string Address { get; set; }
    }
}
