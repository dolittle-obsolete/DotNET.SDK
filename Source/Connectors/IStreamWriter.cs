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
    /// Defines a writer that exposed the capability of writing streams of <see cref="TagDataPoint">tag data points</see>
    /// </summary>
    public interface IStreamWriter
    {
        /// <summary>
        /// Write <see cref="TagDataPoint">tag data points</see> to the stream
        /// </summary>
        /// <param name="dataPoints"><see cref="TagDataPoint">Tag data points</see> to write</param>
        /// <returns><see cref="Task"/> for continuation</returns>
        Task Write(IEnumerable<TagDataPoint> dataPoints);
    }
}