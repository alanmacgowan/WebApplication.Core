using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Core.UI.Models
{
    public class EmployeeViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Country { get; set; }
    }
}