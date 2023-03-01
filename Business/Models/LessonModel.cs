using AppCore.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
#nullable disable
namespace Business.Models
{
    public class LessonModel:RecordBase
    {
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "{0} is required!")]
        [DisplayName("Is Online?")]
        public bool IsOnline { get; set; }

        [DisplayName("Is Online?")]

        public string IsOnlineDisplay { get; set; }
    }
}
