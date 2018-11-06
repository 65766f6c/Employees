// <copyright file="SurchargeCalculator.cs" company="DevCats">
//     Copyright (c) DevCats. All rights reserved.
// </copyright>

namespace Employees.Core.Domain
{
    using Abstractions;

    internal class SurchargeCalculator : ISurchargeCalculator
    {
        private readonly decimal surchargePercentage;
        private readonly decimal maxSurchargePercentage;

        /// <summary>
        /// Initializes a new instance of <see cref="SurchargeCalculator"/> class.
        /// </summary>
        public SurchargeCalculator(decimal surchargePercentage, decimal maxSurchargePercentage)
        {
            this.surchargePercentage = surchargePercentage;
            this.maxSurchargePercentage = maxSurchargePercentage;
        }

        public decimal GetTotalRate(decimal baseRate, int multiplier)
        {
            var totalPercentage = this.surchargePercentage * multiplier;
            if (totalPercentage > maxSurchargePercentage)
            {
                totalPercentage = maxSurchargePercentage;
            }
            return baseRate * ++totalPercentage;
        }
    }
}
