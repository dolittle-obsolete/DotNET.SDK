/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Dolittle.TimeSeries
{
    /// <summary>
    /// Represents an data point for a <see cref="Tag"/> on a <see cref="Source"/>
    /// </summary>
    public class TagDataPoint
    {
        /// <summary>
        /// Gets or sets the <see cref="Tag"/> this value belong to
        /// </summary>
        public Tag Tag { get; set; }

        /// <summary>
        /// Gets or sets the value
        /// </summary>
        public object Value { get; set; }
    }
}