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
using Dolittle.Types;
using Dolittle.TimeSeries.Runtime.Connectors.Grpc.Server;
using static Dolittle.TimeSeries.Runtime.Connectors.Grpc.Server.StreamConnectors;
using Dolittle.Protobuf;
using Dolittle.Logging;

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
        readonly StreamConnectorsConfiguration _configuration;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="StreamConnectors"/>
        /// </summary>
        /// <param name="streamConnectorsClient"><see cref="IClientFor{T}">client for</see> <see cref="StreamConnectorsClient"/> for connecting to runtime</param>
        /// <param name="connectors">Instances of <see cref="IAmAStreamingConnector"/></param>
        /// <param name="configuration"><see cref="StreamConnectorsConfiguration"/> for configuring stream connectors</param>
        /// <param name="logger"><see cref="ILogger"/> for logging</param>
        public StreamConnectors(
            IClientFor<StreamConnectorsClient> streamConnectorsClient,
            IInstancesOf<IAmAStreamingConnector> connectors,
            StreamConnectorsConfiguration configuration,
            ILogger logger)
        {
            _connectors = connectors.ToDictionary(_ => (ConnectorId) Guid.NewGuid(), _ => _);
            _streamConnectorsClient = streamConnectorsClient;
            _configuration = configuration;
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

                var tags = _configuration[_.Value.Name]?.Tags ?? new Tag[0];

                var streamConnector = new StreamConnector
                {
                    Id = _.Key.ToProtobuf(),
                    Name = _.Value.Name,
                };

                streamConnector.Tags.Add(tags.Select(t => t.Value));
                
                _streamConnectorsClient.Instance.Register(streamConnector);
            });
        }
    }
}