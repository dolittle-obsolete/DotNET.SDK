/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Booting;
using Dolittle.Services;

namespace Dolittle.TimeSeries.Connectors
{
    /// <summary>
    /// Represents a <see cref="ICanPerformBootProcedure">boot procedure</see> that hooks up 
    /// </summary>
    public class ConnectorsBootProcedure : ICanPerformBootProcedure
    {
        readonly IPullConnectors _pullConnectors;
        readonly IStreamConnectors _streamConnectors;

        /// <summary>
        /// Initializes a new instance of <see cref="ConnectorsBootProcedure"/>
        /// </summary>
        /// <param name="pullConnectors">System for <see cref="IPullConnectors"/></param>
        /// <param name="streamConnectors">System for <see cref="IStreamConnectors"/></param>
        public ConnectorsBootProcedure(IPullConnectors pullConnectors, IStreamConnectors streamConnectors)
        {
            _pullConnectors = pullConnectors;
            _streamConnectors = streamConnectors;
        }        

        /// <inheritdoc/>
        public bool CanPerform() => EndpointsBootProcedure.EndpointsReady;

        /// <inheritdoc/>
        public void Perform()
        {
            _pullConnectors.Register();
            _streamConnectors.Register();
        }
    }
}