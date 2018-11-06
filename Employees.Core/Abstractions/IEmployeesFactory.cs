// <copyright file="IEmployeesFactory.cs" company="DevCats">
//     Copyright (c) DevCats. All rights reserved.
// </copyright>

namespace Employees.Core.Abstractions
{
    using System.Collections.Generic;
    using Model;

    /// <summary>
    /// Represents domain employees factory
    /// </summary>
    public interface IEmployeesFactory
    {
        /// <summary>
        /// Initializes the domain employees collection
        /// </summary>
        /// <param name="employees">Initial employees</param>
        /// <returns>Target domain employees collection</returns>
        ICollection<IEmployee> Create(ICollection<EmployeeFlat> employees);
    }
}
