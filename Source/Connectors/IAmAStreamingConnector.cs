/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Threading.Tasks;
using Dolittle.TimeSeries.DataPoints;

namespace Dolittle.TimeSeries.Connectors
{
    /// <summary>
    /// Represents a connector type that connects and streams data from the source at the cadence decided by the source
    /// </summary>
    public interface IAmAStreamingConnector
    {
        /// <summary>
        /// Gets the name of the connector
        /// </summary>
        Source Name { get; }

        /// <summary>
        /// Connect to the system with the
        /// </summary>
        /// <param name="configuration"><see cref="StreamConnectorConfiguration">Configuration</see> for the connector</param>
        /// <param name="writer"><see cref="IStreamWriter"/> used for writing <see cref="TagDataPoint"/></param>
        /// <returns><see cref="Task"/> for continuation</returns>
        Task Connect(StreamConnectorConfiguration configuration, IStreamWriter writer);
    }
}