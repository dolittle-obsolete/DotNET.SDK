/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Specifications;
using Dolittle.TimeSeries.DataTypes;

namespace Dolittle.TimeSeries.DataPoints
{
    /// <summary>
    /// Represents the filtering of <see cref="DataPoint{T}">data points</see>
    /// </summary>
    public class DataPointsOf<TValue> where TValue:IMeasurement
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="specification"></param>
        public DataPointsOf(Specification<DataPoint<TValue>> specification)
        {
            ValueSpecification = specification;
        }

        /// <summary>
        /// 
        /// </summary>
        public Specification<TimeSeriesMetadata>    MetadataSpecification { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public Specification<DataPoint<TValue>> ValueSpecification {  get; set; }
    }
}