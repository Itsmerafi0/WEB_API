using API.Utility;
using System.ComponentModel.DataAnnotations;

namespace API.ViewModels.Accounts
{
    public class RegisterVM 
    {

/*        public string NIK { get; set; }
 *        
*/        [Required (ErrorMessage = "Required FirstName")]
        public string FirstName { get; set; }
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Required BirthDate")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Required Gander")]
        public GenderLevel Gender { get; set; }

        [Required(ErrorMessage = "Required HiringDate")]
        public DateTime HiringDate { get; set; }

        [EmailAddress]
        [NIKEmailPhoneValidationAttributeneValidation(nameof(Email))]
        public string Email { get; set; }
        [Phone]
        [NIKEmailPhoneValidationAttributeneValidation(nameof(PhoneNumber))]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Required Major")]

        public string Major { get; set; }

        [Required(ErrorMessage = "Required Degree")]
        public string Degree { get; set; }

        [Range (0, 4 ,ErrorMessage = "GPA Must 1 - 4")]
        public float Gpa { get; set; }

        [Required(ErrorMessage = " Required UniversityCode")]
        public string UniversitasCode { get; set; }

        [Display(Name = "University Name")]
        public string UniversityName { get; set; }

        [PasswordValidate(ErrorMessage = "Password must contain at least 1 Number, 1 UpperCase, 1 Lowercase, 1 Symbols, 6 MinumChar")]
        public string Password { get; set; }
        
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

    }
}
