using System.ComponentModel.DataAnnotations;

namespace PaymentSystemAPI.Models
{
    public class BusinessModel
    {

        public Guid ContactId { get; set; }

        [Required(ErrorMessage = "First Name is Required")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage="Last Name is Required")]
        public string? LastName { get; set; }

        [DateMoreThanOneYear]
        public DateTime DateOfEstablishment { get; set; }

        [Required(ErrorMessage = "Merchant Number is Required")]
        public string? MerchantNumber { get; set; }

        public float TransactionVolume { get; set; }

    }
    public class DateMoreThanOneYear : ValidationAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return "Date value should at least a year";
        }

        protected override ValidationResult IsValid(object objValue,
                                                       ValidationContext validationContext)
        {
            var dateValue = objValue as DateTime? ?? new DateTime();

            //alter this as needed. I am doing the date comparison if the value is not null

            if (dateValue > DateTime.Now.AddDays(-1))
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
            return ValidationResult.Success;
        }
    }

}
