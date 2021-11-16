using PaylocityDeductionCalculator.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PaylocityDeductionCalculator.Models
{
    public class Dependent
    {
        [Required(ErrorMessage = "First name is required.")]
        [DisplayName("Full Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Dependent type is required.")]
        [DisplayName("Person Type")]
        public PersonType PersonType { get; set; }

        public bool DiscountApplied
        {
            get
            {
                if (!string.IsNullOrEmpty(FullName))
                {
                    return FullName.ToLower().StartsWith("a");
                }

                return false;
            }
        }
    }
}