/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Threading.Tasks;
using Dolittle.TimeSeries.DataTypes;

namespace Dolittle.TimeSeries.DataPoints
{
    /// <summary>
    /// Represents an implementation of <see cref="IDataPointPublisher"/>
    /// </summary>
    public class DataPointPublisher : IDataPointPublisher
    {
        /// <inheritdoc/>
        public Task Publish<TValue>(DataPoint<TValue> dataPoint) where TValue:IValue
        {
            return Task.CompletedTask;
        }
    }
}