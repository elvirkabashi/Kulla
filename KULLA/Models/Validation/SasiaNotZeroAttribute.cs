using System;
using System.ComponentModel.DataAnnotations;

public class SasiaNotZeroAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value != null)
        {
            int sasia;
            if (int.TryParse(value.ToString(), out sasia))
            {
                if (sasia == 0)
                {
                    return new ValidationResult("Sasia nuk mund te ket vler zero!");
                }
            }
        }
        return ValidationResult.Success;
    }
}
