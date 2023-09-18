using System.ComponentModel.DataAnnotations;

namespace PaymentSystemAPI.Models
{
    public class CustomerModel
    {
        [Required(ErrorMessage = "NationalID Number is required")]
        public string? NationalIDNumber { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string? LastName { get; set; }

        [DOBValidation]
        [Required(ErrorMessage = "Dateof Birth is required")]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        public string? PhoneNumber { get; set; }
    }


    public class DOBValidation : ValidationAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return "Date of Birth value should at 18 years ";
        }

        protected override ValidationResult IsValid(object objValue,
                                                       ValidationContext validationContext)
        {
            var dateValue = objValue as DateTime? ?? new DateTime();

            //alter this as needed. I am doing the date comparison if the value is not null

            if (dateValue > DateTime.Now.AddDays(-18))
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
            return ValidationResult.Success;
        }
    }
}
