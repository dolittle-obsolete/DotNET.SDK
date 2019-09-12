/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Dolittle.Configuration;

namespace Dolittle.TimeSeries.Connectors
{
    /// <summary>
    /// Provides default configuration for <see cref="PullConnectorsConfiguration"/>
    /// </summary>
    public class PullConnectorsDefaultConfiguration : ICanProvideDefaultConfigurationFor<PullConnectorsConfiguration>
    {
        /// <inheritdoc/>
        public PullConnectorsConfiguration Provide()
        {
            return new PullConnectorsConfiguration(new Dictionary<Source, PullConnectorConfiguration>());
        }
    }

}