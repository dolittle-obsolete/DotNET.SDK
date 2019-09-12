/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Threading.Tasks;
using Grpc.Core;
using Dolittle.TimeSeries.Runtime.Connectors.Grpc.Client;
using static Dolittle.TimeSeries.Runtime.Connectors.Grpc.Client.PullConnector;
using System.Linq;
using Dolittle.Protobuf;

namespace Dolittle.TimeSeries.Connectors
{
    /// <summary>
    /// Represents an implementation of <see cref="PullConnectorBase"/>
    /// </summary>
    public class PullConnectorService : PullConnectorBase
    {
        readonly IPullConnectors _connectors;

        /// <summary>
        /// Initializes a new instance of <see cref="PullConnectorService"/>
        /// </summary>
        /// <param name="connectors">System for <see cref="IPullConnectors"/></param>
        public PullConnectorService(IPullConnectors connectors)
        {
            _connectors = connectors;
        }

        /// <inheritdoc/>
        public override async Task<PullResult> Pull(PullRequest request, ServerCallContext context)
        {           
            var connector = _connectors.GetById(request.ConnectorId.ToGuid());
            var dataPoints = await connector.Pull(request.Tags.Select(_ => (Tag)_));
            var result = new PullResult();
            result.Data.Add(dataPoints.Select(_ => new DataTypes.TagDataPoint {
                Tag = _.Tag,
            }));
            
            return result;
        }
    }
}