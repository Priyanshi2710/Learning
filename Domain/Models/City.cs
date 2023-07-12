using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class City
    {
        [Key]
        public int CityCode { get; set; }
        public required string Cityname { get; set; }

        [Display(Name = "State")]
        public virtual int StateCode { get; set; }

        [ForeignKey("StateCode")]
        public virtual State? State { get; set; }

    }
}
