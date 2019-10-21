/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Dolittle.Clients;
using grpc = Dolittle.TimeSeries.Connectors.Runtime;

namespace Dolittle.TimeSeries.Connectors
{
    /// <summary>
    /// Represents the runtime services having a client representation
    /// </summary>
    public class RuntimeServices : IDefineRuntimeServices
    {
        /// <summary>
        /// Initializes a new instance of <see cref="RuntimeServices"/>
        /// </summary>
        public RuntimeServices()
        {
            Services = new[] {
                new RuntimeServiceDefinition(typeof(grpc.PullConnectors.PullConnectorsClient), grpc.PullConnectors.Descriptor),
                new RuntimeServiceDefinition(typeof(grpc.StreamConnectors.StreamConnectorsClient), grpc.PullConnectors.Descriptor)
            };
        }

        /// <inheritdoc/>
        public IEnumerable<RuntimeServiceDefinition>   Services { get; }
    }
}