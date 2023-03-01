using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable

namespace Business.Models
{
    public class StudentContactModel
    {
        [Required]
        [StringLength(250)]
        public string Email { get; set; }

        [Required]
        [StringLength(1000)]

        //[DataType("email")]
        public string Address { get; set; }
        [DisplayName("Country")]
        public int? CountryId { get; set; }
        public Country Country { get; set; }
        public City City { get; set; }
        [DisplayName ("City")]
        public int? CityId { get; set; }




    }
}
