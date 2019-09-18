/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Machine.Specifications;

namespace Dolittle.TimeSeries.DataTypes.for_MeasurementExtensions
{

    public class when_converting_double_measurement_and_back
    {
        static Measurement<double>   measurement = new Measurement<double> { Value = 42.43, Error = 44.45 };
        static Measurement<double>   result;

        Because of = () => result = measurement.ToProtobuf().ToMeasurement<double>();

        It should_hold_the_correct_value = () => result.Value.ShouldEqual(measurement.Value);
        It should_hold_the_correct_error = () => result.Error.ShouldEqual(measurement.Error);
    }
}