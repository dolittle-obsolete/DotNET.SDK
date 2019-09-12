/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Threading.Tasks;

namespace Dolittle.TimeSeries.Connectors
{
    /// <summary>
    /// Represents a connector type that connects and streams data from the source at the cadence decided by the source
    /// </summary>
    public interface IAmAStreamingConnector
    {
        /// <summary>
        /// The event that represents data being received
        /// </summary>
        event TagDataReceived  DataReceived;

        /// <summary>
        /// Gets the name of the connector
        /// </summary>
        Source Name { get; }

        /// <summary>
        /// Connect to the system 
        /// </summary>
        Task Connect(StreamConnectorConfiguration configuration);
    }
}