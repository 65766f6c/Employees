// <copyright file="UnitTest.cs" company="DevCats">
//     Copyright (c) DevCats. All rights reserved.
// </copyright>

namespace Employees.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core;
    using Core.Abstractions;
    using Core.Model;
    using Xunit;

    public class UnitTest
    {
        private const decimal EarthAnomalisticYearDates = 365.259641m;

        private TestData TestData { get; }
        private IEmployeesManager Manager { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="UnitTest"/> class.
        /// </summary>
        public UnitTest()
        {
            TestData = new TestData();
            Manager = new EmployeesManagerBuilder(TestData.Configuration)
                .AddEmployees(TestData.Employees)
                .Build();
        }

        [Fact]
        public void TestOverallRate()
        {
            var testRate = Manager.Employees.Sum(s => s.CurrentTotalRate);
            var rate = Manager.CurrentTotalRate;
            Assert.True(decimal.Compare(testRate, rate) == decimal.Zero);
        }

        /// <summary>
        /// Tests overall employees rate. Uses the input with iterative methods to compare result with recursive core objects
        /// </summary>
        [Fact]
        public void TestEmployeesRate()
        {
            foreach (var testEmployee in TestData.Employees)
            {
                foreach (var employee in Manager.Employees)
                {
                    if (testEmployee.Id == employee.Id)
                    {
                        // getting target configuration details
                        var details = TestData.Configuration.Details[testEmployee.Type];

                        // getting the base surcharge multiplier
                        var employmentTimeSpan = DateTime.UtcNow - employee.DateOfEmployment;
                        var employmentTotalYearsDecimal = employmentTimeSpan.Days / EarthAnomalisticYearDates;
                        employmentTotalYearsDecimal = Math.Floor(employmentTotalYearsDecimal);
                        var employmentTotalYears = Convert.ToInt32(employmentTotalYearsDecimal);

                        // getting overall employee rate
                        var rate = employee.BaseRate;

                        // getting surcharge based on employment duration
                        if (employmentTotalYears > 0)
                        {
                            var surchargePercents = details.BaseSurchargePercentage * employmentTotalYears;

                            // sadly reducing super percents
                            if (surchargePercents > details.MaxBaseSurchargePercentage)
                            {
                                surchargePercents = details.MaxBaseSurchargePercentage;
                            }

                            // surcharging
                            var surcharge = rate * surchargePercents;
                            rate += surcharge;

                        }

                        // if its a chief with subordinates
                        if (testEmployee.Type != EmployeeType.Employee
                            && employee.Subordinates != null
                            && employee.Subordinates.Any())
                        {
                            ICollection<IEmployee> subordinates = employee.Subordinates;
                            for (int i = 1; i <= details.SubordinatesSurchargeLevel; i++)
                            {
                                var nextLevel = new List<IEmployee>();
                                decimal totalSubordinatesRate = 0;

                                foreach (var subordinate in subordinates)
                                {
                                    totalSubordinatesRate += subordinate.CurrentTotalRate;
                                    if (subordinate.Subordinates == null) continue;
                                    nextLevel.AddRange(subordinate.Subordinates);
                                }

                                // surcharging
                                rate += totalSubordinatesRate * details.SubordinatesSurchargePercentage;

                                if (!nextLevel.Any()) break; // exit
                                subordinates = nextLevel; // going deeper
                            }
                        }

                        Assert.True(decimal.Compare(rate, employee.CurrentTotalRate) == decimal.Zero);
                    }
                    else
                    {
                        Assert.True(decimal.Compare(testEmployee.BaseRate, employee.CurrentTotalRate) == decimal.Zero);
                    }

                    break; // employee is found
                }
            }
        }

        /// <summary>
        /// Tests the accuracy of domain objects initialization
        /// </summary>
        [Fact]
        public void TestEmployeesInitialization()
        {
            foreach (var testEmployee in TestData.Employees)
            {
                foreach (var employee in Manager.Employees)
                {
                    if (testEmployee.Id == employee.Id)
                    {
                        Assert.True(testEmployee.Name == employee.Name);
                        Assert.True(testEmployee.BaseRate == employee.BaseRate);
                        Assert.True(testEmployee.DateOfEmployment == employee.DateOfEmployment);

                        if(testEmployee.Subordinates != null && !testEmployee.Subordinates.Any())
                        {
                            // verifying all subordinates initialized
                            foreach (var subordinate in testEmployee.Subordinates)
                            {
                                var children = employee.Subordinates.Single(s => s.Id == subordinate);
                                Assert.NotNull(children);
                            }
                        }

                        break; // employee is found
                    }
                }
            }
        }
    }
}
