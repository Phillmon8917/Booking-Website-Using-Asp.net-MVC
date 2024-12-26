namespace BookSession.ClientSideValidater
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime date)
            {
                if (date < DateTime.Now.Date)
                {
                    return new ValidationResult("The event cannot be in the past.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
