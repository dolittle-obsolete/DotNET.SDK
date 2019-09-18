/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Dolittle.TimeSeries
{
    /// <summary>
    /// Represents the metadata associated with a <see cref="TimeSeries"/>
    /// </summary>
    public class TimeSeriesMetadata
    {
        /// <summary>
        /// Initializes a new instance of <see cref="TimeSeriesMetadata"/>
        /// </summary>
        /// <param name="timeSeries">The <see cref="TimeSeries"/> the metadata is for</param>
        public TimeSeriesMetadata(TimeSeriesId timeSeries)
        {
            TimeSeries = timeSeries;
        }

        /// <summary>
        /// Gets the <see cref="TimeSeries"/> the metadata is for
        /// </summary>
        public TimeSeriesId TimeSeries { get; }
    }
}