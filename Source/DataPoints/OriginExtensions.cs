/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Specifications;
using Dolittle.TimeSeries.DataTypes;

namespace Dolittle.TimeSeries.DataPoints
{
    /// <summary>
    /// Extends <see cref="DataPointsOf{T}"/> with rules for <see cref="Origin"/>
    /// </summary>
    public static class OriginExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="origin"></param>
        /// <returns></returns>
        public static DataPointsOf<TValue> OriginatingFrom<TValue>(this DataPointsOf<TValue> filter, Origin origin)
            where TValue:IMeasurement
        {
            filter.ValueSpecification = filter.ValueSpecification.And(new OriginOf<TValue>(origin));
            return filter;
        }
    }
}