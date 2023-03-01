using AppCore.Records.Bases;
using DataAccess.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
#nullable disable
namespace Business.Models
{
    public class TeacherModel:RecordBase
    {
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(50)]
        [MinLength(3, ErrorMessage = "{0} must be minimum {1} characters!")]
        [MaxLength(200, ErrorMessage = "{0} must be maximum {1} characters!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(50)]
        [MinLength(3, ErrorMessage = "{0} must be minimum {1} characters!")]
        [MaxLength(200, ErrorMessage = "{0} must be maximum {1} characters!")]

        public string Surname { get; set; }

        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [DisplayName("Registration Date")]
        public DateTime RegistrationDate { get; set; }
		public int? InstrumentId { get; set; }

		public Instrument Instrument { get; set; }

        public bool IsDeleted { get; set; }

        #region Entity dışı özellikler

        public List<int> LessonIds { get; set; }

        public List<int> ClassroomIds { get; set; }

        public List<int> StudentIds { get; set; }
        public TeacherContactModel TeacherContact { get; set; }
        [DisplayName("Instrument Name")]
        public string InstrumentDisplay { get; set; }


        #endregion
    }
}
