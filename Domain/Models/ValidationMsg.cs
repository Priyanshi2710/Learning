using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ValidationMsg
    {
        public string SalaryMsg { get; set; } = "Salary is greater than 500..";
       
        public string Age { get; set; } = "Eligibility 18 years ONLY.";
    }
}
