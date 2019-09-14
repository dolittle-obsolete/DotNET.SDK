/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Dolittle.Clients;
using Dolittle.Services;
using Dolittle.TimeSeries.Runtime.Connectors.Grpc.Client;

namespace Dolittle.TimeSeries.Connectors
{
    /// <summary>
    /// Represents an implementation of <see cref="ICanBindApplicationClientServices"/> - providing application client services
    /// for working with connectors
    /// </summary>
    public class ApplicationClientServices : ICanBindApplicationClientServices
    {
        readonly PullConnectorService _pullConnectorService;
        readonly StreamConnectorService _streamConnectorService;

        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationClientServices"/>
        /// </summary>
        /// <param name="pullConnectorService">Concrete instance of an <see cref="PullConnectorService"/></param>
        /// <param name="streamConnectorService">Concrete instance of an <see cref="StreamConnectorService"/></param>
        public ApplicationClientServices(
            PullConnectorService pullConnectorService,
            StreamConnectorService streamConnectorService)
        {
            _pullConnectorService = pullConnectorService;
            _streamConnectorService = streamConnectorService;
        }

        /// <inheritdoc/>
        public IEnumerable<Service> BindServices()
        {
            return new[] {
                new Service(_pullConnectorService, PullConnector.BindService(_pullConnectorService), PullConnector.Descriptor),
                new Service(_streamConnectorService, StreamConnector.BindService(_streamConnectorService), StreamConnector.Descriptor)
            };
        }
    }
}