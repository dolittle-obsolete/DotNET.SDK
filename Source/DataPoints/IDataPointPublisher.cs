/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Threading.Tasks;
using Dolittle.TimeSeries.DataTypes;

namespace Dolittle.TimeSeries.DataPoints
{
    /// <summary>
    /// Defines a system that is capable of publishing <see cref="DataPoint{T}"/> to the outside world
    /// </summary>
    public interface IDataPointPublisher
    {
        /// <summary>
        /// Publishses a <see cref="DataPoint{T}"/> to the outside world
        /// </summary>
        /// <param name="dataPoint"><see cref="DataPoint{T}"/> to publish</param>
        /// <returns><see cref="Task"/> for continuation</returns>
        Task Publish<TValue>(DataPoint<TValue> dataPoint) where TValue:IValue;
    }
}