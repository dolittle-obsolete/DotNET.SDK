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
using static Dolittle.TimeSeries.Runtime.Connectors.Grpc.Server.PullConnectors;
using Dolittle.Protobuf;

namespace Dolittle.TimeSeries.Connectors
{

    /// <summary>
    /// Represents an implementation of <see cref="IPullConnectors"/>
    /// </summary>
    [Singleton]
    public class PullConnectors : IPullConnectors
    {
        readonly IDictionary<ConnectorId, IAmAPullConnector> _connectors;
        readonly IClientFor<PullConnectorsClient> _pullConnectorsClient;
        readonly PullConnectorsConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of <see cref="PullConnectors"/>
        /// </summary>
        /// <param name="pullConnectorsClient"><see cref="IClientFor{T}">client for</see> <see cref="PullConnectorsClient"/> for connecting to runtime</param>
        /// <param name="connectors">Instances of <see cref="IAmAPullConnector"/></param>
        /// <param name="configuration"><see cref="PullConnectorsConfiguration"/> for configuring pull connectors</param>
        public PullConnectors(IClientFor<PullConnectorsClient> pullConnectorsClient, IInstancesOf<IAmAPullConnector> connectors, PullConnectorsConfiguration configuration)
        {
            _connectors = connectors.ToDictionary(_ => (ConnectorId) Guid.NewGuid(), _ => _);
            _pullConnectorsClient = pullConnectorsClient;
            _configuration = configuration;
        }

        /// <inheritdoc/>
        public IAmAPullConnector GetById(ConnectorId connectorId)
        {
            return _connectors[connectorId];
        }

        /// <inheritdoc/>
        public void Register()
        {
            _connectors.ForEach(_ =>
            {
                var interval = _configuration[_.Value.Name]?.Interval ?? 10000;
                var tags = _configuration[_.Value.Name]?.Tags ?? new Tag[0];

                var pullConnector = new PullConnector
                {
                    Id = _.Key.ToProtobuf(),
                    Name = _.Value.Name,
                    Interval = interval
                };

                pullConnector.Tags.Add(tags.Select(t => t.Value));
                
                _pullConnectorsClient.Instance.Register(pullConnector);
            });
        }
    }
}