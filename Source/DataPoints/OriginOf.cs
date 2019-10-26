/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Specifications;
using Dolittle.TimeSeries.DataTypes;

namespace Dolittle.TimeSeries.DataPoints
{
    /// <summary>
    /// Represents a filter for a specific origin
    /// </summary>
    public class OriginOf<T> : Specification<DataPoint<T>>
        where T:IMeasurement
    {
        /// <summary>
        /// Initializes a new instance of <see cref="OriginOf{T}"/>
        /// </summary>
        /// <param name="origin"><see cref="Origin"/> to filter on</param>
        public OriginOf(Origin origin)
        {
            Origin = origin;
            //Predicate = dataPoint => dataPoint.Origin == origin;
        }

        /// <summary>
        /// Gets the origin for the filter
        /// </summary>
        public Origin Origin { get; }
    }
}