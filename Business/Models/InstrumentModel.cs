using AppCore.Records.Bases;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
#nullable disable

namespace Business.Models
{
    public class InstrumentModel : RecordBase
    {
        [Required(ErrorMessage = "{0} is required!")]
        [DisplayName("Instrument Name")]
        [MinLength(2, ErrorMessage = "{0} must be minimum {1} characters!")] 
        [MaxLength(200, ErrorMessage = "{0} must be maximum {1} characters!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [DisplayName("Unit Price")]
        [Range(0, double.MaxValue, ErrorMessage = "{0} must be 0 or bigeer number!")]
        public double? UnitPrice { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [DisplayName("Stock Amount")]
        [Range(0, int.MaxValue, ErrorMessage = "{0} must be 0 or bigeer number!")]

        public int? StockAmount { get; set; }

        [DisplayName("Price")]
        public string UnitPriceDisplay { get; set; }

        [DisplayName ("Image")]

        public string ImgSrcDisplay { get; set; }

        
        public byte[] Image { get; set; }

        [StringLength(5,ErrorMessage ="{0} must be maximum {1} characters!")]
        public string ImageExtension { get; set; }


    }
}