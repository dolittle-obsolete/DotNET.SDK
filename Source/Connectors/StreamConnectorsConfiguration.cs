/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Dolittle.Configuration;
using Dolittle.TimeSeries.DataPoints;

namespace Dolittle.TimeSeries.Connectors
{
    /// <summary>
    /// Represents the configuration for <see cref="IStreamConnectors"/>
    /// </summary>
    [Name("streamconnectors")]
    public class StreamConnectorsConfiguration : 
        ReadOnlyDictionary<Source, StreamConnectorConfiguration>,
        IConfigurationObject
    {
        /// <summary>
        /// Initializes a new instance of <see cref="StreamConnectorsConfiguration"/>
        /// </summary>
        /// <param name="sources">Configuration instance - passed along to be made immutable</param>
        public StreamConnectorsConfiguration(IDictionary<Source, StreamConnectorConfiguration> sources) : base(sources)
        {
        }
    }
}