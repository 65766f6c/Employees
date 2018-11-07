// <copyright file="TestData.cs" company="DevCats">
//     Copyright (c) DevCats. All rights reserved.
// </copyright>

namespace Employees.Tests
{
    using System;
    using System.Collections.Generic;
    using Core.Model;

    internal class TestData
    {
        public const decimal DefaultBaseRate = 1000000m;

        /// <summary>
        /// Initializes a new instance of <see cref="TestData"/> class.
        /// </summary>
        public TestData()
        {
            Employees = new List<EmployeeFlat>();

            // generating the test data
            int i = 1;

            // base employees
            for (; i <= 10; i++)
            {
                Employees.Add(new EmployeeFlat()
                {
                    Id = i,
                    BaseRate = DefaultBaseRate,
                    DateOfEmployment = DateTime.UtcNow.AddYears(-1 * i),
                    Name = i + " Employee",
                    Type = EmployeeType.Employee
                });
            }

            // managers
            for (; i <= 15; i++)
            {
                Employees.Add(new EmployeeFlat()
                {
                    Id = i,
                    BaseRate = DefaultBaseRate,
                    DateOfEmployment = DateTime.UtcNow.AddYears(-1 * i),
                    Name = i + " Manager",
                    Type = EmployeeType.Manager,
                    Subordinates = new List<long>() { i - 5, i - 10 }
                });
            }
            for (; i <= 20; i++)
            {
                Employees.Add(new EmployeeFlat()
                {
                    Id = i,
                    BaseRate = DefaultBaseRate,
                    DateOfEmployment = DateTime.UtcNow.AddYears(-1 * i),
                    Name = i + " Manager",
                    Type = EmployeeType.Manager,
                    Subordinates = new List<long>() { i - 5 }
                });
            }

            // sales
            for (; i <= 30; i++)
            {
                Employees.Add(new EmployeeFlat()
                {
                    Id = i,
                    BaseRate = DefaultBaseRate,
                    DateOfEmployment = DateTime.UtcNow.AddYears(-1 * i),
                    Name = i + " Sales",
                    Type = EmployeeType.Sales
                });
            }
            for (; i <= 35; i++)
            {
                Employees.Add(new EmployeeFlat()
                {
                    Id = i,
                    BaseRate = DefaultBaseRate,
                    DateOfEmployment = DateTime.UtcNow.AddYears(-1 * i),
                    Name = i + " Sales",
                    Type = EmployeeType.Sales,
                    Subordinates = new List<long>() { i - 5, i - 10 }
                });
            }
            for (; i <= 40; i++)
            {
                Employees.Add(new EmployeeFlat()
                {
                    Id = i,
                    BaseRate = DefaultBaseRate,
                    DateOfEmployment = DateTime.UtcNow.AddYears(-1 * i),
                    Name = i + " Sales",
                    Type = EmployeeType.Sales,
                    Subordinates = new List<long>() { i - 5 }
                });
            }
        }

        public ICollection<EmployeeFlat> Employees { get; }

        public EmployeesConfiguration Configuration { get; } = new EmployeesConfiguration()
        {
            Details = new Dictionary<EmployeeType, EmployeeDetails>()
            {
                {
                    EmployeeType.Employee,
                    new EmployeeDetails()
                    {
                        BaseSurchargePercentage = 0.03m,
                        MaxBaseSurchargePercentage = 0.3m
                    }
                },
                {
                    EmployeeType.Manager,
                    new EmployeeDetails()
                    {
                        BaseSurchargePercentage = 0.05m,
                        MaxBaseSurchargePercentage = 0.4m,
                        SubordinatesSurchargePercentage = 0.005m,
                        SubordinatesSurchargeLevel = 1
                    }
                },
                {
                    EmployeeType.Sales,
                    new EmployeeDetails()
                    {
                        BaseSurchargePercentage = 0.01m,
                        MaxBaseSurchargePercentage = 0.35m,
                        SubordinatesSurchargePercentage = 0.003m
                    }
                }
            }
        };
    }
}
