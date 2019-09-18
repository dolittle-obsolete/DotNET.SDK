/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Machine.Specifications;

namespace Dolittle.TimeSeries.DataTypes.for_ValueExtensions
{
    public class when_converting_int64_measurement_and_back
    {
        static IValue   measurement = new Measurement<Int64> { Value = 42, Error = 43 };
        static Measurement<Int64>   result;

        Because of = () => result = measurement.ToProtobuf().ToMeasurement<Int64>();

        It should_hold_the_correct_value = () => result.Value.ShouldEqual(((Measurement<Int64>)measurement).Value);
        It should_hold_the_correct_error = () => result.Error.ShouldEqual(((Measurement<Int64>)measurement).Error);
    }
}