using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Core.UI.Entities
{
    [Table("Employees")]
    public class Employee : BaseEntity
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Country { get; set; }
    }
}