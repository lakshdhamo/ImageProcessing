using BusinessService.Managers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    public class EmployeeTest
    {
        private readonly EmployeeManager _employeeManager = new EmployeeManager();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AddEmployee_Positive_OneName()
        {
            /// Arrange
            string name = "First";

            /// Act
            var result = _employeeManager.AddEmployee(name);

            /// Asset
            Assert.AreEqual(1, result.Count());
        }

        [Test]
        public void AddEmployee_Positive_MultipleNames()
        {
            /// Arrange
            List<string> lstName = new List<string>() { "First", "Second", "Third" };

            /// Act
            List<string> result = new List<string>();
            foreach (string name in lstName)
            {
                result = _employeeManager.AddEmployee(name);
            }

            /// Asset
            Assert.AreEqual(3, result.Count());
        }

        [Test]
        public void AddEmployee_Negative()
        {
            /// Arrange
            string name = "First";

            /// Act & Asset
            Assert.Throws<ArgumentException>(() => _employeeManager.AddEmployee(null));
        }

    }
}