/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Machine.Specifications;

namespace Dolittle.TimeSeries.DataTypes.for_ValueExtensions
{
    public class when_converting_vector3_as_value_to_value_and_back
    {
        static IValue vector3 = new Vector3
        {
            X = new Measurement<float> { Value = 42.43f, Error = 44.45f }, 
            Y = new Measurement<float> { Value = 46.47f, Error = 48.49f },
            Z = new Measurement<float> { Value = 50.51f, Error = 52.53f }
        };

        static Vector3 result;

        Because of = () => result = vector3.ToProtobuf().ToVector3();

        It should_hold_correct_x_value = () => result.X.Value.ShouldEqual(((Vector3)vector3).X.Value);
        It should_hold_correct_x_error = () => result.X.Error.ShouldEqual(((Vector3)vector3).X.Error);
        It should_hold_correct_y_value = () => result.Y.Value.ShouldEqual(((Vector3)vector3).Y.Value);
        It should_hold_correct_y_error = () => result.Y.Error.ShouldEqual(((Vector3)vector3).Y.Error);
        It should_hold_correct_z_value = () => result.Z.Value.ShouldEqual(((Vector3)vector3).Z.Value);
        It should_hold_correct_z_error = () => result.Z.Error.ShouldEqual(((Vector3)vector3).Z.Error);
    }
}