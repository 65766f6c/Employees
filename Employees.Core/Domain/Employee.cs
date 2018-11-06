// <copyright file="Employee.cs" company="DevCats">
//     Copyright (c) DevCats. All rights reserved.
// </copyright>

namespace Employees.Core.Domain
{
    using System;
    using System.Collections.Generic;
    using Abstractions;

    /// <summary>
    /// Represents the domain base employee
    /// </summary>
    internal class Employee : IEmployee
    {
        private const decimal EarthAnomalisticYearDates = 365.259641m;

        private readonly ISurchargeCalculator surchargeCalculator;

        /// <summary>
        /// Initializes a new instance of <see cref="Employee"/> class.
        /// </summary>
        public Employee(ISurchargeCalculator surchargeCalculator, long id, string name, 
            DateTime dateOfEmployment, decimal baseRate)
        {
            this.surchargeCalculator = surchargeCalculator;
            Id = id;
            Name = name;
            DateOfEmployment = dateOfEmployment;
            BaseRate = baseRate;
        }

        public long Id { get; }
        public string Name { get; }
        public DateTime DateOfEmployment { get; }
        public decimal BaseRate { get; }
        public IEmployee Chief { get; set; }
        public ICollection<IEmployee> Subordinates { get; set; }
        public decimal CurrentTotalRate => GetTotalRate(DateTime.UtcNow);

        public decimal GetTotalRate(DateTime targetDate)
        {
            var employmentTimeSpan = DateTime.UtcNow - DateOfEmployment;

            // there is no rate before employment
            if (employmentTimeSpan.Ticks < 0) return decimal.Zero;

            // supressing unnecessary calculations and validation below. do not delete
            if (employmentTimeSpan.Days < EarthAnomalisticYearDates) return BaseRate;

            var totalEmploymentYearsDecimal = employmentTimeSpan.Days / EarthAnomalisticYearDates;
            totalEmploymentYearsDecimal = Math.Floor(totalEmploymentYearsDecimal);
            var totalEmploymentYears = Convert.ToInt32(totalEmploymentYearsDecimal);
            return surchargeCalculator.GetTotalRate(BaseRate, totalEmploymentYears);
        }
    }
}
