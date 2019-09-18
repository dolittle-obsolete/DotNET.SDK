/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Machine.Specifications;

namespace Dolittle.TimeSeries.DataTypes.for_ValueExtensions
{
    public class when_converting_vector2_as_value_to_value_and_back
    {
        static IValue vector2 = new Vector2
        {
            X = new Measurement<float> { Value = 42.43f, Error = 44.45f }, 
            Y = new Measurement<float> { Value = 46.47f, Error = 48.49f }
        };

        static Vector2 result;

        Because of = () => result = vector2.ToProtobuf().ToVector2();

        It should_hold_correct_x_value = () => result.X.Value.ShouldEqual(((Vector2)vector2).X.Value);
        It should_hold_correct_x_error = () => result.X.Error.ShouldEqual(((Vector2)vector2).X.Error);
        It should_hold_correct_y_value = () => result.Y.Value.ShouldEqual(((Vector2)vector2).Y.Value);
        It should_hold_correct_y_error = () => result.Y.Error.ShouldEqual(((Vector2)vector2).Y.Error);
    }
}