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
    public class PullConnectorConfiguration
    {
        /// <summary>
        /// Initializes a new instance of <see cref="PullConnectorConfiguration"/>
        /// </summary>
        /// <param name="interval">Interval in milliseconds for the pulling</param>
        /// <param name="tags">Collection of <see cref="Tag">tags</see> exposed by the pull connector</param>
        public PullConnectorConfiguration(int interval, IEnumerable<Tag> tags)
        {
            Interval = interval;
            Tags = tags;
        }

        /// <summary>
        /// Gets the pull interval in milliseconds used for the connector
        /// </summary>
        public int Interval { get; }

        /// <summary>
        /// Gets the collection of <see cref="Tag">tags</see> exposed by the pull connector
        /// </summary>
        public IEnumerable<Tag> Tags { get; }
    }
}