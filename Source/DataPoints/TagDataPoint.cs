/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.TimeSeries.DataTypes;

namespace Dolittle.TimeSeries.DataPoints
{
    /// <summary>
    /// Represents a data point for a <see cref="Tag"/> on a <see cref="Source"/>
    /// </summary>
    public class TagDataPoint
    {
        /// <summary>
        /// Initializes a new instance of <see cref="TagDataPoint"/>
        /// </summary>
        /// <param name="tag"><see cref="Tag"/></param>
        /// <param name="value"><see cref="IValue"/></param>
        public TagDataPoint(Tag tag, IValue value)
        {
            Tag = tag;
            Value = value;
        }

        /// <summary>
        /// Gets or sets the <see cref="Tag"/> this value belong to
        /// </summary>
        public Tag Tag { get; }

        /// <summary>
        /// Gets or sets the value
        /// </summary>
        public IValue Value { get; }
    }
}