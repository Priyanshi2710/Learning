using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmpID { get; set; }

        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public required string Firstname { get; set; }

        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public required string Lastname { get; set; }
        public required string Email { get; set; }
        public string? Gender { get; set; }
        public string? MaritalStatus { get; set; }
        public string? EmpPhoto { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Birthdate { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Salary { get; set; }
        [StringLength(250, ErrorMessage = "Name cannot be longer than 250 characters.")]
        public string? Address { get; set; }
        public required string Password { get; set; }

        [Display(Name = "Country")]
        public virtual int CountryCode { get; set; }
        [ForeignKey("CountryCode")]
        public virtual Country? Country { get; set; }

        [Display(Name = "City")]
        public virtual int CityCode { get; set; }
        [ForeignKey("CityCode")]
        public virtual City? City { get; set; }

        [Display(Name = "State")]
        public virtual int StateCode { get; set; }

        [ForeignKey("StateCode")]
        public virtual State? State { get; set; }

        [DataType(DataType.Date)]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Created { get; set; }

        [DataType(DataType.Date)]
        //  [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Updated { get; set; }

    }
}