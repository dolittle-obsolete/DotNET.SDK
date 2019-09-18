/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.TimeSeries.DataTypes.Protobuf;

namespace Dolittle.TimeSeries.DataTypes
{
    /// <summary>
    /// Extension methods for conversion between <see cref="Measurement{T}"/> and <see cref="Measurement"/>
    /// </summary>
    public static class MeasurementExtensions
    {
        /// <summary>
        /// Convert supported primitive types of <see cref="Measurement{T}"/> to <see cref="Measurement"/>
        /// </summary>
        /// <param name="measurement"><see cref="Measurement{T}"/> to convert from</param>
        /// <returns>Converted <see cref="Measurement"/></returns>
        /// <remarks>
        /// Supported primitives:
        /// - float
        /// - double
        /// - int / Int32
        /// - Int64
        /// </remarks>
        public static Measurement ToProtobuf<T>(this Measurement<T> measurement)
        {
            var converted = new Measurement();
            switch (measurement.Value)
            {
                case float _:
                    {
                        var m = measurement as Measurement<float>;
                        converted.FloatValue = m.Value;
                        converted.FloatError = m.Error;
                    }
                    break;
                case double _:
                    {
                        var m = measurement as Measurement<double>;
                        converted.DoubleValue = m.Value;
                        converted.DoubleError = m.Error;
                    }
                    break;
                case int _:
                    {
                        var m = measurement as Measurement<int>;
                        converted.Int32Value = m.Value;
                        converted.Int32Error = m.Error;
                    }
                    break;
                case Int64 _:
                    {
                        var m = measurement as Measurement<Int64>;
                        converted.Int64Value = m.Value;
                        converted.Int64Error = m.Error;
                    }
                    break;
                default: throw new UnsupportedPrimitiveTypeForMeasurement(typeof(T));

            }

            return converted;
        }

        /// <summary>
        /// Convert a protobuf <see cref="Measurement"/> to a <see cref="Measurement{T}"/> of supported primitive types
        /// </summary>
        /// <param name="measurement"><see cref="Measurement"/> to convert from</param>
        /// <typeparam name="T">Type of primitive for the <see cref="Measurement{T}"/></typeparam>
        /// <returns>Converted <see cref="Measurement{T}"/></returns>
         /// <remarks>
        /// Supported primitives:
        /// - float
        /// - double
        /// - int / Int32
        /// - Int64
        /// </remarks>
       public static Measurement<T> ToMeasurement<T>(this Measurement measurement)
        {
            var converted = new Measurement<T>();
            switch (converted.Value)
            {
                case float _:
                    {
                        var m = converted as Measurement<float>;
                        m.Value = measurement.FloatValue;
                        m.Error = measurement.FloatError;
                    }
                    break;
                case double _:
                    {
                        var m = converted as Measurement<double>;
                        m.Value = measurement.DoubleValue;
                        m.Error = measurement.DoubleError;
                    }
                    break;
                case int _:
                    {
                        var m = converted as Measurement<int>;
                        m.Value = measurement.Int32Value;
                        m.Error = measurement.Int32Error;
                    }
                    break;
                case Int64 _:
                    {
                        var m = converted as Measurement<Int64>;
                        m.Value = measurement.Int64Value;
                        m.Error = measurement.Int64Error;
                    }
                    break;
                default: throw new UnsupportedPrimitiveTypeForMeasurement(typeof(T));
            }

            return converted;
        }
    }
}