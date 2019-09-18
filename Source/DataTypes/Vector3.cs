/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Concepts;

namespace Dolittle.TimeSeries.DataTypes
{
    /// <summary>
    /// Represents a 2 dimensional vector
    /// </summary>
    public class Vector3 : Value<Vector3>, IValue
    {
        /// <summary>
        /// Gets or sets the X component
        /// </summary>
        public Measurement<float>   X { get; set; }

        /// <summary>
        /// Gets or sets the Y component
        /// </summary>
        public Measurement<float>   Y { get; set; }

        /// <summary>
        /// Gets or sets the Y component
        /// </summary>
        public Measurement<float>   Z { get; set; }
    }
}