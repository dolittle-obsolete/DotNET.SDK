/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Dolittle.TimeSeries.DataPoints
{
    /// <summary>
    /// Defines a system 
    /// </summary>
    public interface IDataPointsProcessors
    {
        /// <summary>
        /// Register all <see cref="ICanProcessDataPoints">processors</see>
        /// </summary>
        void Register();

        /// <summary>
        /// Get a <see cref="DataPointProcessor"/> by its <see cref="DataPointProcessorId">unique identifier</see>
        /// </summary>
        /// <param name="id"><see cref="DataPointProcessorId"/> to get by</param>
        /// <returns>The <see cref="DataPointProcessor"/> for the id</returns>
        DataPointProcessor GetById(DataPointProcessorId id);
    }
}