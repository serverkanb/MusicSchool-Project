using System.ComponentModel;
#nullable disable

namespace Business.Models
{
    public class CartItemGroupByModel
    {
        public int InstrumentId { get; set; }
        public int UserId { get; set; }

        [DisplayName("Instrument Name")]
        public string InstrumentName { get; set; }
        public string TotalUnitPrice { get; set; }

        public string TotalCount { get; set; }


        [DisplayName("Total Price")]
        public double TotalUnitPriceValue { get; set; }//sum 

        [DisplayName("Total Count")]
        public int TotalCountValue { get; set; }//count
    }
}
