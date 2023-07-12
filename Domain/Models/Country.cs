using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Country
    {
        [Key]
        public int CountryCode { get; set; }
        public required string Countryname { get; set; }
    }
}
