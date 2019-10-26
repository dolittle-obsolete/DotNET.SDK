/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Heads;
using Dolittle.Collections;
using Dolittle.Lifecycle;
using Dolittle.TimeSeries.Connectors.Runtime;
using Dolittle.Types;
using static Dolittle.TimeSeries.Connectors.Runtime.PullConnectors;
using System.Threading.Tasks;
using Dolittle.Protobuf;
using Dolittle.TimeSeries.DataPoints;
using Dolittle.TimeSeries.DataTypes;
using Grpc.Core;

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
        public PullConnectors(
            IClientFor<PullConnectorsClient> pullConnectorsClient,
            IInstancesOf<IAmAPullConnector> connectors,
            PullConnectorsConfiguration configuration)
        {
            _connectors = connectors.ToDictionary(_ => (ConnectorId)Guid.NewGuid(), _ => _);
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

                var pullConnector = new PullConnector
                {
                    Id = _.Key.ToProtobuf(),
                    Name = _.Value.Name,
                    Interval = interval
                };

                Task.Run(async () =>
                {
                    var streamCall = _pullConnectorsClient.Instance.Connect(pullConnector);

                    while (await streamCall.ResponseStream.MoveNext())
                    {
                        var pullRequest = streamCall.ResponseStream.Current;

                        var result = await _.Value.Pull();
                        var tagDataPoints = result.Select(tagDataPoint => tagDataPoint.ToRuntime());
                        var writeMessage = new WriteMessage
                        {
                            ConnectorId = pullConnector.Id
                        };
                        writeMessage.Data.Add(tagDataPoints);
                        await _pullConnectorsClient.Instance.WriteAsync(writeMessage);
                    }
                });
            });
        }
    }
}