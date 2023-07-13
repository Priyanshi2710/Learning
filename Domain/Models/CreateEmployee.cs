using DocumentFormat.OpenXml.Vml.Office;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain.Models
{
    public class CreateEmployee : IValidatableObject
    {
        //[Key]
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //  public int EmpID { get; set; }
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public required string Email { get; set; }
        public string? Gender { get; set; }
        public string? MaritalStatus { get; set; }
        //public IFormFile EmpPhoto { get; set; }

        public DateTime Birthdate { get; set; } = DateTime.MinValue;

        public decimal Salary { get; set; }

        public string? Address { get; set; }
        public required string Password { get; set; }

        public virtual int CountryCode { get; set; }

        public virtual int CityCode { get; set; }

        public virtual int StateCode { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var Today = DateTime.Today;
            if (Today.Year - Birthdate.Year == 18 || Today.Year - Birthdate.Year < 18)
            {
                if (Today.Month < Birthdate.Month)
                {
                   yield return new ValidationResult("Eligibility 18 years ONLY.");
                }
                if (Today.Month == Birthdate.Month)
                {
                    if (Today.Day < Birthdate.Day)
                    {
                        yield return new ValidationResult("Eligibility 18 years ONLY.");
                    }
                }
                yield return new ValidationResult("Eligibility 18 years ONLY.");

            }
            if (Salary < 500)
            {
                yield return new ValidationResult("Invalid salary");
            }
        }
        
    }
}
