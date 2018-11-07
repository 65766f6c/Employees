// <copyright file="EmployeesManager.cs" company="DevCats">
//     Copyright (c) DevCats. All rights reserved.
// </copyright>

namespace Employees.Core.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Abstractions;

    /// <inheritdoc/>
    internal class EmployeesManager : IEmployeesManager
    {
        /// <summary>
        /// Initializes a new instance of <see cref="EmployeesManager"/> class.
        /// </summary>
        public EmployeesManager(IDictionary<long, IEmployee> employees)
        {
            Employees = employees;
        }

        /// <inheritdoc/>
        public IDictionary<long, IEmployee> Employees { get; }

        /// <inheritdoc/>
        public decimal CurrentTotalRate => GetTotalRate(DateTime.UtcNow);

        /// <inheritdoc/>
        public decimal GetTotalRate(DateTime targetDate)
        {
            return Employees.Values.Sum(s => s.GetTotalRate(targetDate));
        }
    }
}
