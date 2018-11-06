// <copyright file="EmployeesManagerBuilder.cs" company="DevCats">
//     Copyright (c) DevCats. All rights reserved.
// </copyright>

namespace Employees.Core
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Abstractions;
    using Domain;
    using Factories;
    using Model;

    /// <inheritdoc/>
    public class EmployeesManagerBuilder : IEmployeesManagerBuilder
    {
        private readonly EmployeesConfiguration configuration;
        private readonly List<EmployeeFlat> employees = new List<EmployeeFlat>();

        /// <summary>
        /// Initializes a new instance of <see cref="EmployeesManagerBuilder"/> class.
        /// </summary>
        public EmployeesManagerBuilder()
        {
            configuration = new EmployeesConfiguration();
        }

        /// <summary>
        /// Initializes a new instance of <see cref="EmployeesManagerBuilder"/> class.
        /// </summary>
        public EmployeesManagerBuilder(EmployeesConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <inheritdoc/>
        public IEmployeesManagerBuilder ConfigureEmployees(Action<EmployeesConfiguration> callback)
        {
            callback.Invoke(configuration);
            return this;
        }

        /// <inheritdoc/>
        public IEmployeesManagerBuilder AddEmployees(ICollection<EmployeeFlat> employees)
        {
            this.employees.AddRange(employees);
            return this;
        }

        /// <inheritdoc/>
        public IEmployeesManager Build()
        {
            var employeeFactory = new EmployeesFactory(configuration);
            var employees = employeeFactory.Create(this.employees);
            return new EmployeesManager(employees);
        }
    }
}
