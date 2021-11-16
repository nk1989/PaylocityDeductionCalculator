using PaylocityDeductionCalculator.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PaylocityDeductionCalculator.Models
{
    public class Deduction
    {
        [DisplayName("Full Name")]
        public string FullName { get; set; }

        [DisplayName("Person Type")]
        public PersonType PersonType { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        [DisplayName("Discount Amount Per PayCheck")]
        public decimal DiscountAmountPerPayCheck { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [DisplayName("Deduction Per PayCheck")]
        public decimal DeductionPerPayCheck { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [DisplayName("Decuction Per Year")]
        public decimal DecuctionPerYear { get; set; }

        public bool DiscountApplied { get; set; }

        [DisplayName("Discount Applied")]
        public Options ToFriendlyString 
        {
            get
            {
                return DiscountApplied ? Options.Yes : Options.No;
            }
        }

        public Deduction()
        {
                
        }
    }
}