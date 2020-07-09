using FluentValidation;
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

    public class EmployeeViewModelValidator : AbstractValidator<EmployeeViewModel>
    {
        public EmployeeViewModelValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.FirstName).Length(0, 200).NotNull();
            RuleFor(x => x.LastName).Length(0, 200).NotNull();
            RuleFor(x => x.BirthDate).LessThan(DateTime.Today).WithMessage("BirthDate must be previous than today.");
            RuleFor(x => x.Country).Length(0, 200);
        }

    }
}