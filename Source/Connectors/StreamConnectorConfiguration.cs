/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Dolittle.TimeSeries.Connectors
{
    /// <summary>
    /// Represents the configuration for a single pull connector
    /// </summary>
    public class StreamConnectorConfiguration
    {
        /// <summary>
        /// Initializes a new instance of <see cref="StreamConnectorConfiguration"/>
        /// </summary>
        /// <param name="tags">Collection of <see cref="Tag">tags</see> exposed by the stream connector</param>
        public StreamConnectorConfiguration(IEnumerable<Tag> tags)
        {
            Tags = tags;
        }

        /// <summary>
        /// Gets the collection of <see cref="Tag">tags</see> exposed by the stream connector
        /// </summary>
        public IEnumerable<Tag> Tags { get; }
    }
}