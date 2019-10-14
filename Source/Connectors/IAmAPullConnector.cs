/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Threading.Tasks;
using Dolittle.TimeSeries.DataPoints;

namespace Dolittle.TimeSeries.Connectors
{
    /// <summary>
    /// Represents a connector type that pulls data from its source
    /// </summary>
    public interface IAmAPullConnector
    {
        /// <summary>
        /// Gets the name of the connector
        /// </summary>
        Source Name {  get; }

        /// <summary>
        /// Pull data from given tags
        /// </summary>
        /// <returns>The data for the <see cref="Tag"/></returns>
        Task<IEnumerable<TagDataPoint>> Pull();
    }
}