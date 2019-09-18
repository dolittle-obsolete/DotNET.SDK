/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Machine.Specifications;

namespace Dolittle.TimeSeries.DataTypes.for_ValueExtensions
{
    public class when_converting_float_measurement_and_back
    {
        static IValue   measurement = new Measurement<float> { Value = 42.43f, Error = 44.45f };
        static Measurement<float>   result;

        Because of = () => result = measurement.ToProtobuf().ToMeasurement<float>();

        It should_hold_the_correct_value = () => result.Value.ShouldEqual(((Measurement<float>)measurement).Value);
        It should_hold_the_correct_error = () => result.Error.ShouldEqual(((Measurement<float>)measurement).Error);
    }
}