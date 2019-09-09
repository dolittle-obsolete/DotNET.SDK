/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Dolittle.Clients;
using Dolittle.Services;
using Dolittle.TimeSeries.Runtime.Connectors.Client.Grpc;

namespace Dolittle.TimeSeries.Connectors
{
    /// <summary>
    /// Represents an implementation of <see cref="ICanBindApplicationClientServices"/> - providing application client services
    /// for working with connectors
    /// </summary>
    public class ApplicationClientServices : ICanBindApplicationClientServices
    {
        readonly PullConnectorService _pullConnectorService;

        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationClientServices"/>
        /// </summary>
        /// <param name="pullConnectorService"></param>
        public ApplicationClientServices(PullConnectorService pullConnectorService)
        {
            _pullConnectorService = pullConnectorService;
        }

        /// <inheritdoc/>
        public IEnumerable<Service> BindServices()
        {
            return new[] {
                new Service(PullConnector.BindService(_pullConnectorService), PullConnector.Descriptor)
            };
        }
    }
}