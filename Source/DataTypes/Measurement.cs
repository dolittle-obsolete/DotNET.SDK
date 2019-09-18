/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Concepts;

namespace Dolittle.TimeSeries.DataTypes
{
    /// <summary>
    /// Represents a measurement
    /// </summary>
    public class Measurement<T> : Value<Measurement<T>>, IValue
    {
        /// <summary>
        /// Implicitly convert from the a value of the type the <see cref="Measurement{T}"/> is 
        /// for to an instance with the value
        /// </summary>
        /// <param name="value">Value to convert from</param>
        /// <remarks>
        /// The <see cref="Measurement{T}.Error">error property</see> will be set to 
        /// the default value for the type
        /// </remarks>
        public static implicit operator Measurement<T>(T value) => new Measurement<T> { Value = value, Error = default };

        /// <summary>
        /// Gets or sets the value
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// Gets or sets the measurement error
        /// </summary>
        /// <remarks>
        /// Typicaly the value is of a number, an error of 0 would mean there is
        /// no deviations to be expected from the value - the value is 100% accurate.
        /// </remarks>
        public T Error { get; set; }
    }
}