/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Machine.Specifications;

namespace Dolittle.TimeSeries.DataTypes.for_MeasurementExtensions
{
    public class when_converting_unsupported_type
    {
        static Exception result;
        Because of = () => result = Catch.Exception(() => new Measurement<object>().ToProtobuf());

        It should_throw_unsupported_primitive_type_for_measurement = () => result.ShouldBeOfExactType<UnsupportedPrimitiveTypeForMeasurement>();
    }
}