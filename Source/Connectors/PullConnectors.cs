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
using Dolittle.TimeSeries.Runtime.Connectors.Server.Grpc;
using Dolittle.Types;
using Google.Protobuf;
using static Dolittle.TimeSeries.Runtime.Connectors.Server.Grpc.PullConnectors;



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

        /// <summary>
        /// Initializes a new instance of <see cref="PullConnectors"/>
        /// </summary>
        /// <param name="pullConnectorsClient"><see cref="IClientFor{T}">client for</see> <see cref="PullConnectorsClient"/> for connecting to runtime</param>
        /// <param name="connectors">Instances of <see cref="IAmAPullConnector"/></param>
        public PullConnectors(IClientFor<PullConnectorsClient> pullConnectorsClient, IInstancesOf<IAmAPullConnector> connectors)
        {
            _connectors = connectors.ToDictionary(_ => (ConnectorId) Guid.NewGuid(), _ => _);
            _pullConnectorsClient = pullConnectorsClient;
        }

        /// <inheritdoc/>
        public void Register()
        {
            _connectors.ForEach(_ =>
            {
                var id = new System.Protobuf.guid();
                id.Value = ByteString.CopyFrom(_.Key.Value.ToByteArray());

                var pullConnector = new PullConnector
                {
                    Id = id,
                    Name = _.Value.Name
                };
                
                _pullConnectorsClient.Instance.Register(pullConnector);
            });
        }
    }
}