/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Dolittle.Clients;
using Dolittle.Services;
using grpc = Dolittle.TimeSeries.Runtime.DataPoints.Grpc.Client;

namespace Dolittle.TimeSeries.DataPoints
{
    /// <summary>
    /// Represents an implementation of <see cref="ICanBindApplicationClientServices"/> - providing application client services
    /// for working with connectors
    /// </summary>
    public class ApplicationClientServices : ICanBindApplicationClientServices
    {
        readonly DataPointProcessorService _dataPointProcessorService;

        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationClientServices"/>
        /// </summary>
        /// <param name="dataPointProcessorService">Concrete instance of an <see cref="DataPointProcessorService"/></param>
        public ApplicationClientServices(
            DataPointProcessorService dataPointProcessorService)
        {
            _dataPointProcessorService = dataPointProcessorService;
        }

        /// <inheritdoc/>
        public IEnumerable<Service> BindServices()
        {
            return new[] {
                new Service(_dataPointProcessorService, grpc.DataPointProcessor.BindService(_dataPointProcessorService), grpc.DataPointProcessor.Descriptor)
            };
        }
    }
}