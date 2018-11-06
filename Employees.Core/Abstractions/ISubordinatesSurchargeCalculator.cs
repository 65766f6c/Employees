// <copyright file="ISubordinatesSurchargeCalculator.cs" company="DevCats">
//     Copyright (c) DevCats. All rights reserved.
// </copyright>

namespace Employees.Core.Abstractions
{
    using System;
    using System.Collections.Generic;

    internal interface ISubordinatesSurchargeCalculator
    {
        decimal GetSurcharge(ICollection<IEmployee> employees, DateTime targetDate);
    }
}
