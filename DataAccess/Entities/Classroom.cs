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
    public class Classroom : RecordBase
    {
		[Required]
		public string Name { get; set; }

		public List<StudentTeacherLessonClassroom> STLCs { get; set; }

	}
}
