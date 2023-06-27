using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Country
    {
        [Key]
        public int CountryCode { get; set; }
        public required string Countryname { get; set; }
    }
}
