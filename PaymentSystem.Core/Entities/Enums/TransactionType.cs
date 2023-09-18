using System.ComponentModel.DataAnnotations;

namespace PaymentSystem.Core.Entities.Enums
{
    public enum TransactionType
    {
        [Display(Name ="Restrictive")]
        RestrictionType = 500,

        [Display(Name = "Non-Restrictive")]
        NoRestrictionType = 505
    }
}
