/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Machine.Specifications;

namespace Dolittle.TimeSeries.DataTypes.for_MeasurementExtensions
{
    public class when_converting_int32_measurement_and_back
    {
        static Measurement<int>   measurement = new Measurement<int> { Value = 42, Error = 44 };
        static Measurement<int>   result;

        Because of = () => result = measurement.ToProtobuf().ToMeasurement<int>();

        It should_hold_the_correct_value = () => result.Value.ShouldEqual(measurement.Value);
        It should_hold_the_correct_error = () => result.Error.ShouldEqual(measurement.Error);
    }
}