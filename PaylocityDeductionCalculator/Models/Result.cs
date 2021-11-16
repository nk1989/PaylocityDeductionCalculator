using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PaylocityDeductionCalculator.Models
{
    public class Result
    {
        public IList<Deduction> Deductions { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        [DisplayName("Total Deduction Per PayCheck")]
        public decimal TotalDeductionPerPayCheck { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        [DisplayName("Total Deduction Per Year")]
        public decimal TotalDeductionPerYear { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        [DisplayName("Total Discount Per PayCheck")]
        public decimal TotalDiscountPerPayCheck { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [DisplayName("Total Discount Per Year")]
        public decimal TotalDiscountPerYear { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        [DisplayName("Total PayCheck After Deductions")]
        public decimal TotalPaycheckAfterDeductions { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [DisplayName("Total Yearly Pay After Deductions")]
        public decimal TotalYearlyPayAfterDeductions { get; set; }

        public Result()
        {
                
        }
    }
}