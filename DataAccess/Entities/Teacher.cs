#nullable disable
using AppCore.Records.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public  class Teacher : RecordBase,ISoftDelete
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

		[Required]
		[StringLength(50)]
		public string Surname { get; set; }

        public DateTime Birthday { get; set; }

		public DateTime RegistrationDate { get; set; }
		public List<StudentTeacherLessonClassroom> STLCs { get; set; }
		public int InstrumentId { get; set; }
		public Instrument Instrument { get; set; }
		public TeacherContact TeacherContact { get; set; }

		public bool IsDeleted { get; set; }





	}
}
