/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Dolittle.Clients;
using Dolittle.TimeSeries.DataPoints.Runtime;

namespace Dolittle.TimeSeries.DataPoints
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
                new RuntimeServiceDefinition(typeof(DataPointProcessors.DataPointProcessorsClient), DataPointProcessors.Descriptor),
                new RuntimeServiceDefinition(typeof(DataPointStream.DataPointStreamClient), DataPointStream.Descriptor)
            };
        }

        /// <inheritdoc/>
        public IEnumerable<RuntimeServiceDefinition>   Services { get; }
    }
}