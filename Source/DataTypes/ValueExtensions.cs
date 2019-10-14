/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Dolittle.TimeSeries.DataTypes
{
    /// <summary>
    /// Extension methods for conversions between <see cref="IValue"/> and <see cref="Protobuf.Value"/>
    /// </summary>
    public static class ValueExtensions
    {
        /// <summary>
        /// Convert from <see cref="IValue"/> of supported types to <see cref="Protobuf.Value"/>
        /// </summary>
        /// <param name="value"><see cref="IValue"/> to convert from</param>
        /// <returns>Converted <see cref="Protobuf.Value"/></returns>
        /// <remarks>
        /// Supported types:
        /// <see cref="Vector2"/>
        /// <see cref="Vector3"/>
        /// <see cref="Measurement"/>
        /// </remarks>
        public static Protobuf.Value ToProtobuf(this IValue value)
        {
            switch (value)
            {
                case Vector2 v:
                    return v.ToProtobuf();
                case Vector3 v:
                    return v.ToProtobuf();
                case Measurement v:
                    return new Protobuf.Value { MeasurementValue = v.ToProtobuf() };
            }
            throw new UnsupportedValueType(value.GetType());
        }

        /// <summary>
        /// Convert from <see cref="Vector2"/> to <see cref="Protobuf.Value"/>
        /// </summary>
        /// <param name="vector2"><see cref="Vector2"/> to convert from</param>
        /// <returns>Converted <see cref="Protobuf.Value"/></returns>
        public static Protobuf.Value ToProtobuf(this Vector2 vector2)
        {
            return new Protobuf.Value
            {
                Vector2Value = new Protobuf.Vector2
                {
                    X = vector2.X.ToProtobuf(),
                    Y = vector2.Y.ToProtobuf()
                }
            };
        }

        /// <summary>
        /// Convert from <see cref="Vector3"/> to <see cref="Protobuf.Value"/>
        /// </summary>
        /// <param name="vector3"><see cref="Vector3"/> to convert from</param>
        /// <returns>Converted <see cref="Protobuf.Value"/></returns>
        public static Protobuf.Value ToProtobuf(this Vector3 vector3)
        {
            return new Protobuf.Value
            {
                Vector3Value = new Protobuf.Vector3
                {
                    X = vector3.X.ToProtobuf(),
                    Y = vector3.Y.ToProtobuf(),
                    Z = vector3.Z.ToProtobuf()
                }
            };
        }

        /// <summary>
        /// Convert from a <see cref="Protobuf.Value"/> to supported implementations of <see cref="Vector2"/>
        /// </summary>
        /// <param name="value"><see cref="Protobuf.Value"/> to convert from</param>
        /// <returns>Converted <see cref="Vector2"/></returns>
        public static Vector2 ToVector2(this Protobuf.Value value)
        {
            return new Vector2
            {
                X = value.Vector2Value.X.ToMeasurement(),
                Y = value.Vector2Value.Y.ToMeasurement()
            };
        }

        /// <summary>
        /// Convert from a <see cref="Protobuf.Value"/> to <see cref="Vector3"/>
        /// </summary>
        /// <param name="value"><see cref="Protobuf.Value"/> to convert from</param>
        /// <returns>Converted <see cref="Vector3"/></returns>
        public static Vector3 ToVector3(this Protobuf.Value value)
        {
            return new Vector3
            {
                X = value.Vector3Value.X.ToMeasurement(),
                Y = value.Vector3Value.Y.ToMeasurement(),
                Z = value.Vector3Value.Z.ToMeasurement()
            };
        }

        /// <summary>
        /// Convert from a <see cref="Protobuf.Value"/> to <see cref="Measurement"/>
        /// </summary>
        /// <param name="value"><see cref="Protobuf.Value"/> to convert from</param>
        /// <returns>Converted <see cref="Measurement"/></returns>
        public static Measurement ToMeasurement(this Protobuf.Value value)
        {
            return value.MeasurementValue.ToMeasurement();
        }
    }
}