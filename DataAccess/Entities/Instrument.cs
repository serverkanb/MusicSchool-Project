using AppCore.Records.Bases;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#nullable disable

namespace DataAccess.Entities
{
    public class Instrument:RecordBase
    {
        
        public string Name { get; set; }

        public double UnitPrice { get; set; }

        public int? StockAmount { get; set; }
        public List<Teacher> Teachers { get; set;}

        [Column (TypeName = "image")]    //tabloda image verisi olarak gidiyor.
        public byte[] Image { get; set; }

        [StringLength (5)]
        public string ImageExtension { get; set; } //dosya uzantısı//binary veriyi çektiğinde dosya uzantısına göre kişiye tipini söyleme için


    }
}
