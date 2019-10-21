/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Clients;
using Dolittle.Collections;
using Dolittle.Lifecycle;
using Dolittle.TimeSeries.Connectors.Runtime;
using Dolittle.Types;
using static Dolittle.TimeSeries.Connectors.Runtime.StreamConnectors;
using System.Threading.Tasks;
using Dolittle.Logging;
using Grpc.Core;

namespace Dolittle.TimeSeries.Connectors
{

    /// <summary>
    /// Represents an implementation of <see cref="IStreamConnectors"/>
    /// </summary>
    [Singleton]
    public class StreamConnectors : IStreamConnectors
    {
        readonly IDictionary<ConnectorId, IAmAStreamingConnector> _connectors;
        readonly IClientFor<StreamConnectorsClient> _streamConnectorsClient;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="StreamConnectors"/>
        /// </summary>
        /// <param name="streamConnectorsClient"><see cref="IClientFor{T}">client for</see> <see cref="StreamConnectorsClient"/> for connecting to runtime</param>
        /// <param name="connectors">Instances of <see cref="IAmAStreamingConnector"/></param>
        /// <param name="logger"><see cref="ILogger"/> for logging</param>
        public StreamConnectors(
            IClientFor<StreamConnectorsClient> streamConnectorsClient,
            IInstancesOf<IAmAStreamingConnector> connectors,
            ILogger logger)
        {
            _connectors = connectors.ToDictionary(_ => (ConnectorId) Guid.NewGuid(), _ => _);
            _streamConnectorsClient = streamConnectorsClient;
            _logger = logger;
        }

        /// <inheritdoc/>
        public IAmAStreamingConnector GetById(ConnectorId connectorId)
        {
            return _connectors[connectorId];
        }

        /// <inheritdoc/>
        public void Register()
        {
            _connectors.ForEach(_ =>
            {
                _logger.Information($"Registering '{_.Value.Name}' with id '{_.Key}'");

                var metadata = new Metadata();
                metadata.Add("streamconnectorid", _.Key.ToString());
                metadata.Add("streamconnectorname", _.Value.Name);

                var streamingCall = _streamConnectorsClient.Instance.Open(metadata);

                Task.Run(async() => await Process(_.Value, streamingCall.RequestStream));
            });
        }

        async Task Process(IAmAStreamingConnector connector, IClientStreamWriter<StreamTagDataPoints> requestStream)
        {
            var streamWriter = new StreamWriter(requestStream);
            await connector.Connect(streamWriter);
        }
    }
}