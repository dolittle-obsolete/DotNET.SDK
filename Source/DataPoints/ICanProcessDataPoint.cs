/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Threading.Tasks;

namespace Dolittle.TimeSeries.DataPoints
{
    /// <summary>
    /// Defines a processor of <see cref="DataPoint{T}"/>
    /// </summary>
    public interface ICanProcessDataPoint<TValue>
    {
        /// <summary>
        /// Gets the <see cref="DataPointsOf{T}">filter</see> to use
        /// </summary>
        DataPointsOf<TValue> Filter { get; }

        /// <summary>
        /// Process a data point
        /// </summary>
        /// <param name="dataPoint"><see cref="DataPoint{T}"/> to process</param>
        /// <returns><see cref="Task"/> for continuation</returns>
        Task Process(DataPoint<TValue> dataPoint);
    }
}