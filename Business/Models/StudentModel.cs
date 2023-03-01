using AppCore.Records.Bases;
using DataAccess.Enum;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
#nullable disable

namespace Business.Models
{
    #region Entityden gelen özellikler
    public class StudentModel:RecordBase
    {
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(50)]
        [DisplayName("Student Name")]
        [MinLength(3, ErrorMessage = "{0} must be minimum {1} characters!")] 
        [MaxLength(200, ErrorMessage = "{0} must be maximum {1} characters!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(50)]
        [DisplayName("Student Surname")]
        [MinLength(3, ErrorMessage = "{0} must be minimum {1} characters!")]
        [MaxLength(200, ErrorMessage = "{0} must be maximum {1} characters!")]
        public string Surname { get; set; }

        public DateTime? Birthday { get; set; }

        [DisplayName("Gender")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage ="{0} is required!")]
        [StringLength(50)]
        [DisplayName("Parent Name")]
        [MinLength(3, ErrorMessage = "{0} must be minimum {1} characters!")]
        [MaxLength(200, ErrorMessage = "{0} must be maximum {1} characters!")]
        public string ParentName { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(50)]
        [DisplayName("Parent Surname")]
        [MinLength(3, ErrorMessage = "{0} must be minimum {1} characters!")]
        [MaxLength(200, ErrorMessage = "{0} must be maximum {1} characters!")]
        public string ParentSurname { get; set; }

 
        [Range(0, int.MaxValue, ErrorMessage = "{0} must be 0 or bigeer number!")]

        [DisplayName("Taken Lesson Count")]

        public int TakenLessonCount { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [DisplayName("Registration Date")]
        public DateTime RegistrationDate { get; set; }

        #endregion
        #region Entity dışı özellikler

        public List<int> LessonIds { get; set; }

        public List<int> ClassroomIds { get; set; }

        public List<int> TeacherIds { get; set; }

        public StudentContactModel StudentContact { get; set; }

        [DisplayName("City")]

        public string CityNameDisplay { get; set; }
        [DisplayName("Country")]

        public string CountryNameDisplay { get; set; }


        [DisplayName("Classroom")]
        public string ClassroomNameDisplay { get; set; }

        [DisplayName("Lesson")]
        public string LessonNameDisplay { get; set; }

        [DisplayName("Teacher")]
        public string TeacherNameDisplay { get; set; }

        #endregion

    }
}
