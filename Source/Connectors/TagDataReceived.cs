/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dolittle.TimeSeries.Connectors
{
    /// <summary>
    /// Represents the callback for when a <see cref="TagDataPoint"/> is received
    /// </summary>
    /// <param name="dataPoint"><see cref="TagDataPoint"/> received</param>
    public delegate Task TagDataReceived(IEnumerable<TagDataPoint> dataPoint);
}