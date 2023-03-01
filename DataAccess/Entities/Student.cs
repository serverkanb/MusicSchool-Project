#nullable disable
using AppCore.Records.Bases;
using DataAccess.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Student : RecordBase
	{
		[Required]
		[StringLength(50)]
		public string Name { get; set; }//push
		[Required]
		[StringLength(50)]
		public string Surname { get; set; }

		public DateTime? Birthday { get; set; }

		public Gender Gender { get; set; }
		[Required]
		[StringLength(50)]
		public string ParentName { get; set; }
		[Required]
		[StringLength(50)]
		public string ParentSurname { get; set; }

		public int TakenLessonCount { get; set; }
		[Required]
		public DateTime RegistrationDate { get; set; }

		public List<StudentTeacherLessonClassroom> STLCs { get; set; }

        public StudentContact StudentContact { get; set; }






    }
}
