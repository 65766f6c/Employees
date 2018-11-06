// <copyright file="IEmployeesManager.cs" company="DevCats">
//     Copyright (c) DevCats. All rights reserved.
// </copyright>

namespace Employees.Core.Abstractions
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the employees manager. Provides an employees collection and total employees rate calculation methods
    /// </summary>
    public interface IEmployeesManager
    {
        /// <summary>
        /// Gets the employees collection
        /// </summary>
        ICollection<IEmployee> Employees { get; }

        /// <summary>
        /// Gets the current total employees rate
        /// </summary>
        decimal CurrentTotalRate { get; }

        /// <summary>
        /// Returns the total employees rate
        /// </summary>
        /// <param name="targetDate">Target date</param>
        /// <returns>Total rate</returns>
        decimal GetTotalRate(DateTime targetDate);
    }
}
