// <copyright file="ISurchargeCalculator.cs" company="DevCats">
//     Copyright (c) DevCats. All rights reserved.
// </copyright>

namespace Employees.Core.Abstractions
{
    internal interface ISurchargeCalculator
    {
        decimal GetTotalRate(decimal baseRate, int multiplier);
    }
}
