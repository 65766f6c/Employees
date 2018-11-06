// <copyright file="IEmployee.cs" company="DevCats">
//     Copyright (c) DevCats. All rights reserved.
// </copyright>

namespace Employees.Core.Abstractions
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the employee
    /// </summary>
    public interface IEmployee
    {
        /// <summary>
        /// Gets or sets the identity
        /// </summary>
        long Id { get; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets or sets the date of employment
        /// </summary>
        DateTime DateOfEmployment { get; }

        /// <summary>
        /// Gets or sets the base rate
        /// </summary>
        decimal BaseRate { get; }

        /// <summary>
        /// Gets or sets the current total rate
        /// </summary>
        decimal CurrentTotalRate { get; }

        /// <summary>
        /// Gets or sets the chief
        /// </summary>
        IEmployee Chief { get; set; }

        /// <summary>
        /// Gets or sets the subordinates
        /// </summary>
        ICollection<IEmployee> Subordinates { get; set; }

        /// <summary>
        /// Returns the total rate
        /// </summary>
        /// <param name="targetDate">Target date</param>
        /// <returns>Total rate</returns>
        decimal GetTotalRate(DateTime targetDate);
    }
}
