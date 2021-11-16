using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaylocityDeductionCalculator.Configs;
using PaylocityDeductionCalculator.Models;
using PaylocityDeductionCalculator.Services;

namespace PaylocityDeductionCalculator.UniTests
{
    [TestClass]
    public class DeductionCalculatorUnitTest
    {
        private static EmployeeConfig employeeConfig;
        private static Employee employee;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            employeeConfig = EmployeeConfig.GetInstance;
            DeductionCalculator.Init(employeeConfig);
        }

        [TestMethod]
        public void EmployeeDeductionUnitTest()
        {
            // Arrange
            employee = new Employee()
            {
                FirstName = "Jim",
                LastName = "Smith",
                WagePerPayCheck = employeeConfig.EmployeeWagePerPayCheck,
                YearlyWage = employeeConfig.EmployeeWagePerPayCheck * employeeConfig.NumberOfPayChecks,
                PayChecksPerYear = employeeConfig.NumberOfPayChecks,
                PersonType = Enums.PersonType.Employee
            };

            //Act
            var result = DeductionCalculator.CalculateDeductions(employee);
            
            //Assert
            Assert.AreEqual("38.46", result.TotalDeductionPerPayCheck.ToString("0.00"));
            Assert.AreEqual("1961.54", result.TotalPaycheckAfterDeductions.ToString("0.00"));
            Assert.AreEqual("51000.00", result.TotalYearlyPayAfterDeductions.ToString("0.00"));
            Assert.IsFalse(result.Deductions[0].DiscountApplied);
        }

        [TestMethod]
        public void DependentDeductionUnitTest()
        {
            // Arrange
            employee = new Employee()
            {
                FirstName = "Jim",
                LastName = "Smith",
                WagePerPayCheck = employeeConfig.EmployeeWagePerPayCheck,
                YearlyWage = employeeConfig.EmployeeWagePerPayCheck * employeeConfig.NumberOfPayChecks,
                PayChecksPerYear = employeeConfig.NumberOfPayChecks,
                PersonType = Enums.PersonType.Employee
            };

            employee.Dependents.Add(new Dependent() { FullName = "Amy Smith", PersonType = Enums.PersonType.Spouse });

            //Act
            var result = DeductionCalculator.CalculateDeductions(employee);

            //Assert
            //employee
            Assert.AreEqual("55.77", result.TotalDeductionPerPayCheck.ToString("0.00"));
            Assert.AreEqual("1944.23", result.TotalPaycheckAfterDeductions.ToString("0.00"));
            Assert.AreEqual("50550.00", result.TotalYearlyPayAfterDeductions.ToString("0.00"));
            Assert.IsFalse(result.Deductions[0].DiscountApplied);

            //dependent
            Assert.AreEqual("450.00", result.Deductions[1].DecuctionPerYear.ToString("0.00"));
            Assert.AreEqual("17.31", result.Deductions[1].DeductionPerPayCheck.ToString("0.00"));
            Assert.AreEqual("1.92", result.Deductions[1].DiscountAmountPerPayCheck.ToString("0.00"));
            Assert.IsTrue(result.Deductions[1].DiscountApplied);
        }
    }
}
