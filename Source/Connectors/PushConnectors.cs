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
using static Dolittle.TimeSeries.Connectors.Runtime.PushConnectors;
using System.Threading.Tasks;
using Dolittle.Logging;
using Grpc.Core;

namespace Dolittle.TimeSeries.Connectors
{

    /// <summary>
    /// Represents an implementation of <see cref="IPushConnectors"/>
    /// </summary>
    [Singleton]
    public class PushConnectors : IPushConnectors
    {
        readonly IDictionary<ConnectorId, IAmAPushConnector> _connectors;
        readonly IClientFor<PushConnectorsClient> _pushConnectorsClient;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="PushConnectors"/>
        /// </summary>
        /// <param name="pushConnectorsClient"><see cref="IClientFor{T}">client for</see> <see cref="PushConnectorsClient"/> for connecting to runtime</param>
        /// <param name="connectors">Instances of <see cref="IAmAPushConnector"/></param>
        /// <param name="logger"><see cref="ILogger"/> for logging</param>
        public PushConnectors(
            IClientFor<PushConnectorsClient> pushConnectorsClient,
            IInstancesOf<IAmAPushConnector> connectors,
            ILogger logger)
        {
            _connectors = connectors.ToDictionary(_ => (ConnectorId)Guid.NewGuid(), _ => _);
            _pushConnectorsClient = pushConnectorsClient;
            _logger = logger;
        }

        /// <inheritdoc/>
        public IAmAPushConnector GetById(ConnectorId connectorId)
        {
            return _connectors[connectorId];
        }

        /// <inheritdoc/>
        public void Register()
        {
            _connectors.ForEach(_ =>
            {
                Task.Run(async () =>
                {
                    _logger.Information($"Registering '{_.Value.Name}' with id '{_.Key}'");

                    var metadata = new Metadata();
                    metadata.Add("pushconnectorid", _.Key.ToString());
                    metadata.Add("pushconnectorname", _.Value.Name);

                    try
                    {
                        var streamingCall = _pushConnectorsClient.Instance.Open(metadata);

                        await Process(_.Value, streamingCall.RequestStream);
                    }
                    catch
                    {
                        Environment.Exit(1);
                    }
                });
            });
        }

        async Task Process(IAmAPushConnector connector, IClientStreamWriter<PushTagDataPoints> requestStream)
        {
            var streamWriter = new StreamWriter(requestStream);
            await connector.Connect(streamWriter);
        }
    }
}