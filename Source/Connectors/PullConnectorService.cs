/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Threading.Tasks;
using Dolittle.TimeSeries.Runtime.Connectors.Client.Grpc;
using Google.Protobuf;
using Grpc.Core;
using static Dolittle.TimeSeries.Runtime.Connectors.Client.Grpc.PullConnector;

namespace Dolittle.TimeSeries.Connectors
{
    /// <summary>
    /// Represents an implementation of <see cref="PullConnectorBase"/>
    /// </summary>
    public class PullConnectorService : PullConnectorBase
    {
        readonly IPullConnectors _connectors;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectors"></param>
        public PullConnectorService(IPullConnectors connectors)
        {
            _connectors = connectors;
        }

        /// <inheritdoc/>
        public override Task<PullResult> Pull(PullRequest request, ServerCallContext context)
        {           
            var connector = _connectors.GetById(new Guid(request.ConnectorId.Value.ToByteArray()));
            connector.Pull(null);
            
            return Task.FromResult(new PullResult());            
        }
    }
}