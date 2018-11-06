// <copyright file="EmployeeDetails.cs" company="DevCats">
//     Copyright (c) DevCats. All rights reserved.
// </copyright>

namespace Employees.Core.Model
{
    using System.ComponentModel;

    /// <summary>
    /// Represents employee details
    /// </summary>
    public class EmployeeDetails
    {
        /// <summary>
        /// Gets or sets the base surcharge percentage
        /// </summary>
        public decimal BaseSurchargePercentage { get; set; }

        /// <summary>
        /// Gets or sets the maximum base surcharge percentage
        /// </summary>
        public decimal MaxBaseSurchargePercentage { get; set; }

        /// <summary>
        /// Gets or sets the subordinates surcharge percentage
        /// </summary>
        public decimal SubordinatesSurchargePercentage { get; set; }

        /// <summary>
        /// Gets or sets the subordinates surcharge level. The default is max value
        /// </summary>
        public int SubordinatesSurchargeLevel { get; set; } = int.MaxValue;
    }
}
