// <copyright file="EmployeesConfiguration.cs" company="DevCats">
//     Copyright (c) DevCats. All rights reserved.
// </copyright>

namespace Employees.Core.Model
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents the employee configuration
    /// </summary>
    public class EmployeesConfiguration
    {
        /// <summary>
        /// Gets or sets the details. Represents the collection of type/detail pairs
        /// </summary>
        public IDictionary<EmployeeType, EmployeeDetails> Details { get; set; } = new Dictionary<EmployeeType, EmployeeDetails>();
    }
}
