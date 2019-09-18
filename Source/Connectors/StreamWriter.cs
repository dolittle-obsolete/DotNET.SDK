/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dolittle.Protobuf;
using Dolittle.TimeSeries.DataPoints;
using Dolittle.TimeSeries.DataTypes;
using Dolittle.TimeSeries.Runtime.Connectors.Grpc.Client;
using Grpc.Core;

namespace Dolittle.TimeSeries.Connectors
{
    /// <summary>
    /// Represents an implementation of <see cref="IStreamWriter"/>
    /// </summary>
    public class StreamWriter : IStreamWriter
    {
        readonly IServerStreamWriter<StreamTagDataPoints> _serverStreamWriter;

        /// <summary>
        /// Initializes a new instance of <see cref="StreamWriter"/>
        /// </summary>
        /// <param name="serverStreamWriter">The <see cref="IServerStreamWriter{T}">server stream</see> to write to</param>
        public StreamWriter(IServerStreamWriter<StreamTagDataPoints> serverStreamWriter)
        {
            _serverStreamWriter = serverStreamWriter;
        }

        /// <inheritdoc/>
        public async Task Write(IEnumerable<TagDataPoint> dataPoints)
        {
            var streamTagDataPoints = new StreamTagDataPoints();
            streamTagDataPoints.DataPoints.Add(dataPoints.Select(_ => new Runtime.DataPoints.Grpc.TagDataPoint
            {
                Tag = _.Tag,
                    Value = _.Value.ToProtobuf()
            }));

            await _serverStreamWriter.WriteAsync(streamTagDataPoints);
        }
    }
}