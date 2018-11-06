// <copyright file="IEmployeesManagerBuilder.cs" company="DevCats">
//     Copyright (c) DevCats. All rights reserved.
// </copyright>

namespace Employees.Core.Abstractions
{
    using System;
    using System.Collections.Generic;
    using Model;

    /// <summary>
    /// Represents the employee manager builder
    /// </summary>
    public interface IEmployeesManagerBuilder
    {
        /// <summary>
        /// Configurates employees
        /// </summary>
        /// <param name="callback">Initial delegate</param>
        IEmployeesManagerBuilder ConfigureEmployees(Action<EmployeesConfiguration> callback);

        /// <summary>
        /// Adds employees
        /// </summary>
        /// <param name="employees">Target employees</param>
        IEmployeesManagerBuilder AddEmployees(ICollection<EmployeeFlat> employees);

        /// <summary>
        /// Initializes the employee manager
        /// </summary>
        /// <returns>Target manager</returns>
        IEmployeesManager Build();

    }
}
