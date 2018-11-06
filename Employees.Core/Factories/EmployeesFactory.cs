// <copyright file="EmployeesFactory.cs" company="DevCats">
//     Copyright (c) DevCats. All rights reserved.
// </copyright>

namespace Employees.Core.Factories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Abstractions;
    using Domain;
    using Model;

    /// <inheritdoc/>
    internal class EmployeesFactory : IEmployeesFactory
    {
        private readonly EmployeesConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of <see cref="EmployeesFactory"/> class.
        /// </summary>
        public EmployeesFactory(EmployeesConfiguration configuration)
        {
            // detecting invalid configuration
            foreach (var details in configuration.Details)
            {
                if (details.Value.BaseSurchargePercentage <= decimal.Zero
                    || details.Value.MaxBaseSurchargePercentage <= decimal.Zero
                    || details.Value.BaseSurchargePercentage > 1m
                    || details.Value.MaxBaseSurchargePercentage > 1m)
                {
                    throw new InvalidOperationException("Invalid configuration detected");
                }

                if (details.Key != EmployeeType.Employee
                    && (details.Value.SubordinatesSurchargePercentage <= decimal.Zero
                        || details.Value.SubordinatesSurchargePercentage > 1m
                        || details.Value.SubordinatesSurchargeLevel < 1))
                {
                    throw new InvalidOperationException("Invalid chief configuration detected");
                }
            }

            this.configuration = configuration;
        }

        /// <inheritdoc/>
        public ICollection<IEmployee> Create(ICollection<EmployeeFlat> input)
        {
            var result = new Dictionary<long, IEmployee>();
            var surchargeCalculators = new Dictionary<EmployeeType, ISurchargeCalculator>();
            var subordinatesSurchargeCalculators = new Dictionary<EmployeeType, ISubordinatesSurchargeCalculator>();

            // initializing calculators
            foreach (var type in input.Select(s => s.Type).Distinct())
            {
                var employeeDetails = this.configuration.Details[type];
                var surchargeCalculator = new SurchargeCalculator(employeeDetails.BaseSurchargePercentage,
                    employeeDetails.MaxBaseSurchargePercentage);
                surchargeCalculators.Add(type, surchargeCalculator);

                if (type != EmployeeType.Employee)
                {
                    var subordinatesSurchargeCalculator = new SubordinatesSurchargeCalculator(
                        employeeDetails.SubordinatesSurchargePercentage, 
                        employeeDetails.SubordinatesSurchargeLevel);
                    subordinatesSurchargeCalculators.Add(type, subordinatesSurchargeCalculator);
                }
            }

            // initializing employees
            foreach (var employeeFlat in input)
            {
                var surchargeCalculator = surchargeCalculators[employeeFlat.Type];
                IEmployee employee = new Employee(surchargeCalculator, employeeFlat.Id, employeeFlat.Name,
                    employeeFlat.DateOfEmployment, employeeFlat.BaseRate);

                if (employeeFlat.Type != EmployeeType.Employee)
                {
                    // decorating employee
                    var subordinatesSurchargeCalculator = subordinatesSurchargeCalculators[employeeFlat.Type];
                    employee = new ChiefEmployee(employee, subordinatesSurchargeCalculator);
                }

                result.Add(employee.Id, employee);
            }

            // setting subordinates
            foreach (var employeeFlat in input)
            {
                if (employeeFlat.Subordinates == null || !employeeFlat.Subordinates.Any()) continue;

                var chief = result[employeeFlat.Id];
                chief.Subordinates = new List<IEmployee>();

                // setting subordinates
                foreach (var subordinateId in employeeFlat.Subordinates)
                {
                    var subordinate = result[subordinateId];

                    // detecting logical errors
                    if (subordinate.Chief != null)
                    {
                        throw new InvalidOperationException("Multiple chiefs detected");
                    }

                    subordinate.Chief = chief;
                    chief.Subordinates.Add(subordinate);
                }
            }

            // one more loop to prevent subordinates closure
            foreach (var employee in result.Values)
            {
                if (HasSubordinatesClosure(employee, employee.Subordinates))
                {
                    throw new InvalidOperationException("Subordinates closure detected");
                }
            }

            return result.Values;
        }

        private bool HasSubordinatesClosure(IEmployee employee, ICollection<IEmployee> subordinates)
        {
            if (subordinates == null) return false;

            foreach (var subordinate in subordinates)
            {
                // searching the closure by chief reference. can be switched to identities
                if (employee == subordinate) return true; // recursion exit

                if (subordinate.Subordinates != null && subordinate.Subordinates.Any())
                {
                    // going deeper
                    var found = HasSubordinatesClosure(employee, subordinate.Subordinates);
                    if (found) return true;
                }
            }

            return false;
        }
    }
}
