using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace PaylocityDeductionCalculator.Configs
{
    public sealed class EmployeeConfig
    {
        private static readonly object Instancelock = new object();
        private static EmployeeConfig instance = null;

        public readonly decimal EmployeeWagePerPayCheck;
        public readonly int NumberOfPayChecks;
        public readonly decimal EmployeeBenefitCostPerYear;
        public readonly decimal DependentBenefitCostPerYear;
        public readonly decimal DiscountPercent;

        private EmployeeConfig()
        {
            EmployeeWagePerPayCheck = Convert.ToDecimal(ConfigurationManager.AppSettings["EmployeeWagePerPayCheck"]);
            NumberOfPayChecks = Convert.ToInt32(ConfigurationManager.AppSettings["NumberOfPayChecks"]);
            EmployeeBenefitCostPerYear = Convert.ToDecimal(ConfigurationManager.AppSettings["EmployeeBenefitCostPerYear"]);
            DependentBenefitCostPerYear = Convert.ToDecimal(ConfigurationManager.AppSettings["DependentBenefitCostPerYear"]);
            DiscountPercent = Convert.ToDecimal(ConfigurationManager.AppSettings["DiscountPercent"]);
        }

        public static EmployeeConfig GetInstance
        {
            get
            {
                if (instance == null)
                {
                    lock (Instancelock)
                    {
                        if (instance == null)
                        {
                            instance = new EmployeeConfig();
                        }
                    }
                }
                return instance;
            }
        }
    }
}