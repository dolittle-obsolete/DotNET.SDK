/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Dolittle.TimeSeries.DataPoints
{
    /// <summary>
    /// Represents the starting point for creating filters for <see cref="DataPoint{T}">data points</see>
    /// </summary>
    public class DataPoints
    {
        /// <summary>
        /// Filter of a specific type of <see cref="DataPoint{T}"/>
        /// </summary>
        /// <returns><see cref="DataPointsOf{T}"/> filter</returns>
        public static DataPointsOf<T> Of<T>() => new DataPointsOf<T>(new DataPointsOfSpecification<T>());
    }
}