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
    public class Vector2 : Value<Vector2>, IValue
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Vector2"/>
        /// </summary>
        public Vector2()
        {
            X = new Measurement();
            Y = new Measurement();
        }

        /// <summary>
        /// Gets or sets the X component
        /// </summary>
        public Measurement   X { get; set; }

        /// <summary>
        /// Gets or sets the Y component
        /// </summary>
        public Measurement   Y { get; set; }
    }
}