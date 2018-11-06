// <copyright file="EmployeeFlat.cs" company="DevCats">
//     Copyright (c) DevCats. All rights reserved.
// </copyright>

namespace Employees.Core.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the flat employee
    /// </summary>
    public class EmployeeFlat
    {
        /// <summary>
        /// Gets or sets the identity
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the type
        /// </summary>
        public EmployeeType Type { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the date of employment
        /// </summary>
        public DateTime DateOfEmployment { get; set; }

        /// <summary>
        /// Gets or sets the base rate
        /// </summary>
        public decimal BaseRate { get; set; }

        /// <summary>
        /// Gets or sets subordinates
        /// </summary>
        public ICollection<long> Subordinates { get; set; }
    }
}
