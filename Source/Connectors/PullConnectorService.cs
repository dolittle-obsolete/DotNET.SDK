/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Threading.Tasks;
using Dolittle.TimeSeries.Runtime.Connectors.Client.Grpc;
using Grpc.Core;
using static Dolittle.TimeSeries.Runtime.Connectors.Client.Grpc.PullConnector;

namespace Dolittle.TimeSeries.Connectors
{
    /// <summary>
    /// Represents an implementation of <see cref="PullConnectorBase"/>
    /// </summary>
    public class PullConnectorService : PullConnectorBase
    {
        /// <inheritdoc/>
        public override Task<PullResult> Pull(PullRequest request, ServerCallContext context)
        {
            System.Console.WriteLine("PULLING");
            return Task.FromResult(new PullResult());            
        }
    }
}