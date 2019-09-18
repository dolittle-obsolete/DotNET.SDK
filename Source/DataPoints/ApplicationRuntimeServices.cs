/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Dolittle.Clients;
using grpc = Dolittle.TimeSeries.Runtime.DataPoints.Grpc.Server;

namespace Dolittle.TimeSeries.DataPoints
{
    /// <summary>
    /// Represents the runtime services having a client representation
    /// </summary>
    public class ApplicationRuntimeServices : IDefineApplicationRuntimeServices
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationRuntimeServices"/>
        /// </summary>
        public ApplicationRuntimeServices()
        {
            Services = new[] {
                new RuntimeServiceDefinition(typeof(grpc.DataPointProcessors.DataPointProcessorsClient), grpc.DataPointProcessors.Descriptor),
                new RuntimeServiceDefinition(typeof(grpc.DataPointStream.DataPointStreamClient), grpc.DataPointStream.Descriptor)
            };
        }

        /// <inheritdoc/>
        public IEnumerable<RuntimeServiceDefinition>   Services { get; }
    }
}