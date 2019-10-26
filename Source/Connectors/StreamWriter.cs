/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dolittle.TimeSeries.DataPoints;
using Dolittle.TimeSeries.DataTypes;
using Dolittle.TimeSeries.Connectors.Runtime;
using Grpc.Core;

namespace Dolittle.TimeSeries.Connectors
{
    /// <summary>
    /// Represents an implementation of <see cref="IStreamWriter"/>
    /// </summary>
    public class StreamWriter : IStreamWriter
    {
        readonly IClientStreamWriter<StreamTagDataPoints> _serverStreamWriter;

        /// <summary>
        /// Initializes a new instance of <see cref="StreamWriter"/>
        /// </summary>
        /// <param name="clientStreamWriter">The <see cref="IClientStreamWriter{T}">server stream</see> to write to</param>
        public StreamWriter(IClientStreamWriter<StreamTagDataPoints> clientStreamWriter)
        {
            _serverStreamWriter = clientStreamWriter;
        }

        /// <inheritdoc/>
        public async Task Write(IEnumerable<TagDataPoint> dataPoints)
        {
            var streamTagDataPoints = new StreamTagDataPoints();
            streamTagDataPoints.DataPoints.Add(dataPoints.Select(_ => _.ToRuntime()));
            await _serverStreamWriter.WriteAsync(streamTagDataPoints).ConfigureAwait(false);
        }
    }
}