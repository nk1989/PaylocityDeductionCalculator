using PaylocityDeductionCalculator.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PaylocityDeductionCalculator.Models
{
    public class Employee
    {
		[Required(ErrorMessage = "First name is required.")]
		[DisplayName("First Name")]
		[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only letters are allowed in first name")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Last name is required.")]
		[DisplayName("Last Name")]
		[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only letters are allowed in last name")]
		public string LastName { get; set; }

		[DisplayName("Salary (Per PayCheck)")]
		public decimal WagePerPayCheck { get; set; }

		[DisplayName("Salary (Per Year)")]
		public decimal YearlyWage { get; set; }

		[DisplayName("Pay checks (Per Year)")]
		public int PayChecksPerYear { get; set; }

		public PersonType PersonType { get; set; } = PersonType.Employee;

        public bool DiscountApplied
		{
			get
			{
				if (!string.IsNullOrEmpty(FirstName))
				{
					return FirstName.ToLower().StartsWith("a");
				}

				return false;
			}
		}

		public IList<Dependent> Dependents { get; set; }

		public Employee()
		{
			Dependents = new List<Dependent>();
		}
	}
}