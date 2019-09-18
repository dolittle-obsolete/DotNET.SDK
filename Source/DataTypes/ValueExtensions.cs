/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Reflection;
using Dolittle.TimeSeries.DataTypes.Protobuf;

namespace Dolittle.TimeSeries.DataTypes
{
    /// <summary>
    /// Extension methods for conversions between <see cref="IValue"/> and <see cref="Value"/>
    /// </summary>
    public static class ValueExtensions
    {
        /// <summary>
        /// Convert from <see cref="IValue"/> of supported types to <see cref="Value"/>
        /// </summary>
        /// <param name="value"><see cref="IValue"/> to convert from</param>
        /// <returns>Converted <see cref="Value"/></returns>
        /// <remarks>
        /// Supported types:
        /// <see cref="Vector2"/>
        /// <see cref="Vector3"/>
        /// <see cref="Measurement{T}"/>        
        /// </remarks>
        public static Value ToProtobuf(this IValue value)
        {
            switch (value)
            {
                case Vector2 _:
                    return ((Vector2) value).ToProtobuf();
                case Vector3 _:
                    return ((Vector3) value).ToProtobuf();
            }
            var type = value.GetType();
            if (type.IsGenericType)
            {
                var genericArguments = type.GetGenericArguments();
                if (typeof(Measurement<>).MakeGenericType(genericArguments[0]).IsAssignableFrom(type))
                {
                    var method = typeof(MeasurementExtensions).GetMethod(
                        "ToProtobuf", 
                        BindingFlags.Public|BindingFlags.Static).MakeGenericMethod(genericArguments[0]);
                    var converted = new Value
                    {
                        MeasurementValue = method.Invoke(null, new[] { value }) as Measurement
                    };
                    return converted;
                }
            }

            throw new UnsupportedValueType(value.GetType());
        }

        /// <summary>
        /// Convert from <see cref="Vector2"/> to <see cref="Value"/>
        /// </summary>
        /// <param name="vector2"><see cref="Vector2"/> to convert from</param>
        /// <returns>Converted <see cref="Value"/></returns>
        public static Value ToProtobuf(this Vector2 vector2)
        {
            return new Value
            {
                Vector2Value = new Protobuf.Vector2
                {
                    X = vector2.X.ToProtobuf(),
                    Y = vector2.Y.ToProtobuf()
                    }
            };
        }

        /// <summary>
        /// Convert from <see cref="Vector3"/> to <see cref="Value"/>
        /// </summary>
        /// <param name="vector3"><see cref="Vector3"/> to convert from</param>
        /// <returns>Converted <see cref="Value"/></returns>
        public static Value ToProtobuf(this Vector3 vector3)
        {
            return new Value
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
        /// Convert from a <see cref="Value"/> to supported implementations of <see cref="Vector2"/>
        /// </summary>
        /// <param name="value"><see cref="Value"/> to convert from</param>
        /// <returns>Converted <see cref="Vector2"/></returns>
        public static Vector2 ToVector2(this Value value)
        {
            return new Vector2
            {
                X = value.Vector2Value.X.ToMeasurement<float>(),
                    Y = value.Vector2Value.Y.ToMeasurement<float>()
            };
        }

        /// <summary>
        /// Convert from a <see cref="Value"/> to <see cref="Vector3"/>
        /// </summary>
        /// <param name="value"><see cref="Value"/> to convert from</param>
        /// <returns>Converted <see cref="Vector3"/></returns>
        public static Vector3 ToVector3(this Value value)
        {
            return new Vector3
            {
                X = value.Vector3Value.X.ToMeasurement<float>(),
                    Y = value.Vector3Value.Y.ToMeasurement<float>(),
                    Z = value.Vector3Value.Z.ToMeasurement<float>()
            };
        }

        /// <summary>
        /// Convert from a <see cref="Value"/> to <see cref="Measurement{T}"/>
        /// </summary>
        /// <param name="value"><see cref="Value"/> to convert from</param>
        /// <returns>Converted <see cref="Measurement{T}"/></returns>
        public static Measurement<T> ToMeasurement<T>(this Value value)
        {
            return value.MeasurementValue.ToMeasurement<T>();
        }
    }
}