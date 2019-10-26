/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Specifications;
using Dolittle.TimeSeries.DataTypes;

namespace Dolittle.TimeSeries.DataPoints
{
    /// <summary>
    /// Represents the base <see cref="Specification{T}"/> for a <see cref="DataPoint{T}"/> of a
    /// specific type
    /// </summary>
    public class DataPointsOfSpecification<TValue> : Specification<DataPoint<TValue>>
        where TValue:IMeasurement
    {

    }
}