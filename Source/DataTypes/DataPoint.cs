/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Dolittle.TimeSeries.DataTypes
{
    /// <summary>
    /// Represents a datapoint in a <see cref="TimeSeries"/>
    /// </summary>
    public class DataPoint<T> where T:IValue 
    {
        /// <summary>
        /// Gets or sets the <see cref="TimeSeries"/> the <see cref="DataPoint{T}"/> belongs to
        /// </summary>
        public TimeSeriesId TimeSeries {  get; set; }

        /// <summary>
        /// Gets or sets the value for the <see cref="DataPoint{T}"/>
        /// </summary>
        public T Value {  get; set; }

        /// <summary>
        /// Gets or sets <see cref="Timestamp"/> in EPOCH microseconds
        /// </summary>
        public Timestamp Timestamp {  get; set; }
    }
}