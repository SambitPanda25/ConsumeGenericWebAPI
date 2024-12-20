using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ConsumeGenericWebAPI.ViewModels
{
    public class EmployeeVM
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Employee Name")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Only alphabetic characters are allowed.")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DOB { get; set; }

        [Required]
        [Display(Name = "Mobile Number")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid mobile number. It must be 10 digits.")]
        public string MobileNo { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
