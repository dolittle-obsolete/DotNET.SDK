/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Dolittle.Configuration;

namespace Dolittle.TimeSeries.Connectors
{
    /// <summary>
    /// Provides default configuration for <see cref="StreamConnectorsConfiguration"/>
    /// </summary>
    public class StreamConnectorsDefaultConfiguration : ICanProvideDefaultConfigurationFor<StreamConnectorsConfiguration>
    {
        /// <inheritdoc/>
        public StreamConnectorsConfiguration Provide()
        {
            return new StreamConnectorsConfiguration(new Dictionary<Source, StreamConnectorConfiguration>());
        }
    }

}