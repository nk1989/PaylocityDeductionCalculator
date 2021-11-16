using PaylocityDeductionCalculator.Configs;
using PaylocityDeductionCalculator.Enums;
using PaylocityDeductionCalculator.Models;
using PaylocityDeductionCalculator.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PaylocityDeductionCalculator.Controllers
{
    public class HomeController : Controller
    {
        private EmployeeConfig employeeConfig { get; }

        public HomeController()
        {
            employeeConfig = EmployeeConfig.GetInstance;
            DeductionCalculator.Init(employeeConfig);
        }

        [HttpGet]
        public ActionResult Index()
        {
            Employee employee = new Employee();
            employee.WagePerPayCheck = employeeConfig.EmployeeWagePerPayCheck;
            employee.YearlyWage = employeeConfig.NumberOfPayChecks * employeeConfig.EmployeeWagePerPayCheck;
            employee.PayChecksPerYear = employeeConfig.NumberOfPayChecks;

            return View(employee);
        }

        [HttpPost]
        public ActionResult Index(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return View(employee);
            }

            TempData["employee"] = employee;
            return RedirectToAction("AddDependent");
        }

        [HttpGet]
        public ActionResult AddDependent()
        {
            Employee employee = (Employee)TempData.Peek("employee");
            if (employee == null) return View("Index");

            return View(employee);
        }

        [HttpPost]
        public ActionResult AddDependent(FormCollection formCollection)
        {
            var employee = (Employee)TempData["employee"];
            if (employee == null) return RedirectToAction("Index");

            if (formCollection.Keys.Count > 0)
            {
                string[] dependentNames = formCollection["dependentName"].Split(',');
                string[] dependentTypes = formCollection["dependentType"].Split(',');

                for (int i = 0; i < dependentNames.Length; i++)
                {
                    string dependentFullName = dependentNames[i];
                    PersonType personType = (PersonType)Enum.Parse(typeof(PersonType), !string.IsNullOrEmpty(dependentTypes[i]) ? dependentTypes[i] : PersonType.Unknown.ToString());
                    employee.Dependents.Add(new Dependent() { FullName = dependentFullName, PersonType = personType });
                }
            }

            var result = DeductionCalculator.CalculateDeductions(employee);
            return View("Result", result);
        }
    }
}