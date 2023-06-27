using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Domain.Models
{
    public class CreateEmployee
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

        public DateTime Birthdate { get; set; }

        public decimal Salary { get; set; }
      
        public string? Address { get; set; }
        public required string Password { get; set; }

        public virtual int CountryCode { get; set; }
       
        public virtual int CityCode { get; set; }
       
        public virtual int StateCode { get; set; }

    }
}
