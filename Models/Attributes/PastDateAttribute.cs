using System.ComponentModel.DataAnnotations;
namespace BloodBank.Models.Attributes
{
    public class PastDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is DateOnly date)
            {
                return date < DateOnly.FromDateTime(DateTime.Today);
            }
            return false;
        }
    }
}
