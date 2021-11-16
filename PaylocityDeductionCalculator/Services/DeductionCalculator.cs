using PaylocityDeductionCalculator.Configs;
using PaylocityDeductionCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaylocityDeductionCalculator.Services
{
    public static class DeductionCalculator
    {
        private static EmployeeConfig config;

        public static void Init(EmployeeConfig employeeConfig)
        {
            config = employeeConfig;
        }

        public static Result CalculateDeductions(Employee employee)
        {
            List<Deduction> deductions = new List<Deduction>();

            var employeeDeductionPerPayCheck = config.EmployeeBenefitCostPerYear / config.NumberOfPayChecks;
            var employeeDiscountPerPayCheck = employee.DiscountApplied ? employeeDeductionPerPayCheck * config.DiscountPercent / 100 : 0;

            var employeeDeduction = new Deduction()
            {
                FullName = $"{employee.FirstName} {employee.LastName}",
                PersonType = employee.PersonType,
                DeductionPerPayCheck = employee.DiscountApplied ? employeeDeductionPerPayCheck - employeeDiscountPerPayCheck : employeeDeductionPerPayCheck,
                DiscountAmountPerPayCheck = employeeDiscountPerPayCheck,
                DecuctionPerYear = employee.DiscountApplied ? (employeeDeductionPerPayCheck - employeeDiscountPerPayCheck) * config.NumberOfPayChecks : employeeDeductionPerPayCheck * config.NumberOfPayChecks,
                DiscountApplied = employee.DiscountApplied
            };

            deductions.Add(employeeDeduction);

            foreach (var dependent in employee.Dependents)
            {
                var dependentDeductionPerPayCheck = config.DependentBenefitCostPerYear / config.NumberOfPayChecks;
                var dependentDiscountPerPayCheck = dependent.DiscountApplied ? dependentDeductionPerPayCheck * config.DiscountPercent / 100 : 0;

                var dependentDeduction = new Deduction()
                {
                    FullName = dependent.FullName,
                    PersonType = dependent.PersonType,
                    DeductionPerPayCheck = dependent.DiscountApplied ? dependentDeductionPerPayCheck - dependentDiscountPerPayCheck : dependentDeductionPerPayCheck,
                    DiscountAmountPerPayCheck = dependentDiscountPerPayCheck,
                    DecuctionPerYear = dependent.DiscountApplied ? (dependentDeductionPerPayCheck - dependentDiscountPerPayCheck) * config.NumberOfPayChecks : dependentDeductionPerPayCheck * config.NumberOfPayChecks,
                    DiscountApplied = dependent.DiscountApplied
                };

                deductions.Add(dependentDeduction);
            }

            var result = new Result()
            {
                Deductions = deductions,
                TotalDeductionPerPayCheck = deductions.Sum(item => item.DeductionPerPayCheck),
                TotalDeductionPerYear = deductions.Sum(item => item.DeductionPerPayCheck) * config.NumberOfPayChecks,
                TotalDiscountPerPayCheck = deductions.Sum(item => item.DiscountAmountPerPayCheck),
                TotalDiscountPerYear = deductions.Sum(item => item.DiscountAmountPerPayCheck) * config.NumberOfPayChecks,
                TotalPaycheckAfterDeductions = config.EmployeeWagePerPayCheck - deductions.Sum(item => item.DeductionPerPayCheck),
                TotalYearlyPayAfterDeductions = config.EmployeeWagePerPayCheck * config.NumberOfPayChecks - deductions.Sum(item => item.DeductionPerPayCheck) * config.NumberOfPayChecks
            };

            return result;
        }
    }
}