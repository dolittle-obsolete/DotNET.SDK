/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Specifications;

namespace Dolittle.TimeSeries.DataPoints
{
    /// <summary>
    /// Represents the filtering of <see cref="DataPoint{T}">data points</see>
    /// </summary>
    public class DataPointsOf<TValue>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="specification"></param>
        public DataPointsOf(Specification<DataPoint<TValue>> specification)
        {
            Specification = specification;
        }

        /// <summary>
        /// 
        /// </summary>
        public Specification<DataPoint<TValue>> Specification {  get; set; }
    }
}