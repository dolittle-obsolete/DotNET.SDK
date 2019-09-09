/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Dolittle.TimeSeries.Connectors
{
    /// <summary>
    /// Defines a system for working with <see cref="IAmAPullConnector"/>
    /// </summary>
    public interface IPullConnectors
    {
        /// <summary>
        /// Register all pull connectors
        /// </summary>
        void Register();
    }
}