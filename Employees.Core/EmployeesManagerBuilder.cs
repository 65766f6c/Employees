// <copyright file="EmployeesManagerBuilder.cs" company="DevCats">
//     Copyright (c) DevCats. All rights reserved.
// </copyright>

namespace Employees.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using Abstractions;
    using Domain;
    using Factories;
    using Model;

    /// <inheritdoc/>
    public class EmployeesManagerBuilder : IEmployeesManagerBuilder
    {
        private readonly List<EmployeeFlat> employees = new List<EmployeeFlat>();
        private EmployeesConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of <see cref="EmployeesManagerBuilder"/> class.
        /// </summary>
        public EmployeesManagerBuilder()
        {
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
            if(configuration == null) configuration = new EmployeesConfiguration();
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
            if (configuration == null)
            {
                throw new InvalidOperationException("Not configured. Pass EmployeesConfiguration to the constructor or use the ConfigureEmployees method before building a manager");
            }
            if (!this.employees.Any())
            {
                throw new InvalidOperationException("Employees doesn't presented. Use the AddEmployees method before building a manager");
            }

            var employeeFactory = new EmployeesFactory(configuration);
            var employees = employeeFactory.Create(this.employees);
            return new EmployeesManager(employees);
        }
    }
}
