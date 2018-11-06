// <copyright file="ChiefEmployee.cs" company="DevCats">
//     Copyright (c) DevCats. All rights reserved.
// </copyright>

namespace Employees.Core.Domain
{
    using System;
    using System.Collections.Generic;
    using Abstractions;

    /// <summary>
    /// Represents the domain chief employee
    /// </summary>
    internal class ChiefEmployee : IEmployee
    {
        private readonly IEmployee baseEmployee;
        private readonly ISubordinatesSurchargeCalculator surchargeCalculator;

        /// <summary>
        /// Initializes a new instance of <see cref="ChiefEmployee"/> class.
        /// </summary>
        public ChiefEmployee(IEmployee baseEmployee, ISubordinatesSurchargeCalculator surchargeCalculator)
        {
            this.baseEmployee = baseEmployee;
            this.surchargeCalculator = surchargeCalculator;
        }

        public long Id => baseEmployee.Id;
        public string Name => baseEmployee.Name;
        public DateTime DateOfEmployment => baseEmployee.DateOfEmployment;
        public decimal BaseRate => baseEmployee.BaseRate;
        public IEmployee Chief { get; set; }
        public ICollection<IEmployee> Subordinates { get; set; }
        public decimal CurrentTotalRate => GetTotalRate(DateTime.UtcNow);

        public decimal GetTotalRate(DateTime targetDate)
        {
            var baseTotalRate = baseEmployee.GetTotalRate(targetDate);
            var surcharge = surchargeCalculator.GetSurcharge(Subordinates, targetDate);
            return baseTotalRate + surcharge;
        }
    }
}
