/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Linq;
using System.Threading.Tasks;
using Dolittle.Logging;
using Dolittle.Protobuf;
using Dolittle.TimeSeries.Runtime.Connectors.Grpc.Client;
using Grpc.Core;
using static Dolittle.TimeSeries.Runtime.Connectors.Grpc.Client.StreamConnector;

namespace Dolittle.TimeSeries.Connectors
{
    /// <summary>
    /// Represents an implementation of <see cref="StreamConnectorBase"/>
    /// </summary>
    public class StreamConnectorService : StreamConnectorBase
    {
        readonly ILogger _logger;
        private readonly IStreamConnectors _connectors;
        private readonly StreamConnectorsConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of <see cref="StreamConnectorService"/>
        /// </summary>
        /// <param name="connectors">System for <see cref="IStreamConnectors"/></param>
        /// <param name="configuration"><see cref="StreamConnectorsConfiguration">Configuration</see></param>
        /// <param name="logger"><see cref="ILogger"/> for logging</param>
        public StreamConnectorService(
            IStreamConnectors connectors,
            StreamConnectorsConfiguration configuration,
            ILogger logger)
        {
            _logger = logger;
            _connectors = connectors;
            _configuration = configuration;
        }

        /// <inheritdoc/>
        public override async Task Connect(StreamRequest request, IServerStreamWriter<StreamTagDataPoints> responseStream, ServerCallContext context)
        {
            var id = request.ConnectorId.ToGuid();
            _logger.Information($"Connect '{id}'");
            var connector = _connectors.GetById(id);
            if (!_configuration.ContainsKey(connector.Name)) await Task.CompletedTask;

            var configuration = _configuration[connector.Name];

            connector.DataReceived += async (dataPoints) =>
            {
                var streamTagDataPoints = new StreamTagDataPoints();
                streamTagDataPoints.DataPoints.Add(dataPoints.Select(_ => new DataTypes.TagDataPoint
                {
                    Tag = _.Tag
                }));

                await responseStream.WriteAsync(streamTagDataPoints);
            };

            await connector.Connect(configuration);

            _logger.Information($"Stream Disconnected");
        }

    }
}