#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class CartItemModel
    {
        public int InstrumentId { get; set; }
        public int UserId { get; set; }

        [DisplayName("Instrument Name")]
        public string InstrumentName { get; set; }

        [DisplayName("Unit Price")]
        public double UnitPrice { get; set; }
    }
}
