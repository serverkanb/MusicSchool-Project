#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Enum;

namespace Business.Models
{
    public class AccountRegisterModel
    {
        [Required(ErrorMessage = "{0} is required!")]
        [MaxLength(20, ErrorMessage = "{0} must be maximum {1} characters")]
        [MinLength(5, ErrorMessage = "{0} must be minimum {1}characters")]
        [DisplayName("User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [MaxLength(10, ErrorMessage = "{0} must be maximum {1} characters")]
        [MinLength(3, ErrorMessage = "{0} must be minimum {1}characters")]
        [Compare("ConfirmPassword", ErrorMessage = "Passwords do not match")]

        public string Password { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [MaxLength(10, ErrorMessage = "{0} must be maximum {1} characters")]
        [MinLength(3, ErrorMessage = "{0} must be minimum {1}characters")]
        [DisplayName("Confirm Password")]

        public string ConfirmPassword { get; set; }

        public int TeacherId { get; set; }
        public int StudentId { get; set; }

    }
}
