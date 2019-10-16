/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Machine.Specifications;

namespace Dolittle.TimeSeries.DataTypes.for_ValueExtensions
{

    public class when_converting_double_measurement_and_back
    {
        static IValue   measurement = new Measurement { Value = 42.43, Error = 44.45 };
        static Measurement   result;

        Because of = () => result = measurement.ToProtobuf().ToMeasurement();

        It should_hold_the_correct_value = () => result.Value.ShouldEqual(((Measurement)measurement).Value);
        It should_hold_the_correct_error = () => result.Error.ShouldEqual(((Measurement)measurement).Error);
    }
}