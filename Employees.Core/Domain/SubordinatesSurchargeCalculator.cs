// <copyright file="SubordinatesSurchargeCalculator.cs" company="DevCats">
//     Copyright (c) DevCats. All rights reserved.
// </copyright>

namespace Employees.Core.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Abstractions;

    internal class SubordinatesSurchargeCalculator : ISubordinatesSurchargeCalculator
    {
        private readonly decimal subordinatesSurchargePercentage;
        private readonly decimal subordinatesSurchargeLevel;

        /// <summary>
        /// Initializes a new instance of <see cref="SubordinatesSurchargeCalculator"/> class.
        /// </summary>
        public SubordinatesSurchargeCalculator(decimal subordinatesSurchargePercentage, 
            decimal subordinatesSurchargeLevel)
        {
            this.subordinatesSurchargePercentage = subordinatesSurchargePercentage;
            this.subordinatesSurchargeLevel = subordinatesSurchargeLevel;
        }

        /// <summary>
        /// Recursively calculates the subordinates surcharge
        /// </summary>
        /// <param name="employees">Initial subordinates</param>
        /// <param name="targetDate">Target date</param>
        /// <returns>Total surcharge</returns>
        public decimal GetSurcharge(ICollection<IEmployee> employees, DateTime targetDate)
        {
            if (employees == null || !employees.Any()) return 0; // supressing calculations
            var subordinatesRate = GetSubordinatesRateInternal(employees, targetDate, 1);
            return subordinatesRate * subordinatesSurchargePercentage;
        }

        /// <summary>
        /// Recursively calculates the subordinates surcharge
        /// </summary>
        /// <param name="employees">Initial subordinates</param>
        /// <param name="targetDate">Target date</param>
        /// <param name="currentLevel">Current subordinates level</param>
        /// <returns>Total surcharge</returns>
        private decimal GetSubordinatesRateInternal(ICollection<IEmployee> employees, DateTime targetDate, int currentLevel)
        {
            decimal result = 0;

            foreach (var employee in employees)
            {
                result += employee.GetTotalRate(targetDate);
            }

            // breaking the recursion
            if (++currentLevel <= subordinatesSurchargeLevel)
            {
                foreach (var employee in employees)
                {
                    if (employee.Subordinates == null || !employee.Subordinates.Any()) continue;

                    // going deeper
                    result += GetSubordinatesRateInternal(employee.Subordinates, targetDate, currentLevel);
                }
            }

            return result;
        }
    }
}
