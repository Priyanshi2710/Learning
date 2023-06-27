using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class State
    {
        [Key]
        public int StateCode { get; set; }
        public required string StateName { get; set; }

        [Display(Name = "Country")]
        public virtual int CountryCode { get; set; }
        [ForeignKey("CountryCode")]
        public virtual Country? Country { get; set; }
    }
}
