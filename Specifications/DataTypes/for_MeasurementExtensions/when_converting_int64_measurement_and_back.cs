/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Machine.Specifications;

namespace Dolittle.TimeSeries.DataTypes.for_MeasurementExtensions
{
    public class when_converting_int65_measurement_and_back
    {
        static Measurement<Int64>   measurement = new Measurement<Int64> { Value = 42, Error = 44 };
        static Measurement<Int64>   result;

        Because of = () => result = measurement.ToProtobuf().ToMeasurement<Int64>();

        It should_hold_the_correct_value = () => result.Value.ShouldEqual(measurement.Value);
        It should_hold_the_correct_error = () => result.Error.ShouldEqual(measurement.Error);
    }
}