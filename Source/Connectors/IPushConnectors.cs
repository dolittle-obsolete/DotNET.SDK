/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Dolittle.TimeSeries.Connectors
{
    /// <summary>
    /// Defines a system for working with <see cref="IAmAPushConnector"/>
    /// </summary>
    public interface IPushConnectors
    {
        /// <summary>
        /// Register all pull connectors
        /// </summary>
        void Register();

        /// <summary>
        /// Get a <see cref="IAmAPullConnector"/> by its <see cref="ConnectorId"/>
        /// </summary>
        /// <param name="connectorId"><see cref="ConnectorId"/> representing the connector</param>
        /// <returns>Instance of <see cref="IAmAPushConnector"/></returns>
        IAmAPushConnector GetById(ConnectorId connectorId);
    }
}