/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Threading.Tasks;
using Dolittle.Logging;
using Dolittle.Protobuf;
using Dolittle.TimeSeries.Runtime.DataPoints.Grpc.Client;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Value = Dolittle.TimeSeries.DataTypes.Protobuf.Value;
using Measurement = Dolittle.TimeSeries.DataTypes.Protobuf.Measurement;
using static Dolittle.TimeSeries.Runtime.DataPoints.Grpc.Client.DataPointProcessor;
using System;
using System.Reflection;
using Dolittle.TimeSeries.DataTypes;

namespace Dolittle.TimeSeries.DataPoints
{
    /// <summary>
    /// Represents an implementation of <see cref="DataPointProcessorBase"/>
    /// </summary>
    public class DataPointProcessorService : DataPointProcessorBase
    {
        readonly IDataPointsProcessors _processors;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="DataPointProcessorService"/>
        /// </summary>
        /// <param name="processors">All <see cref="IDataPointsProcessors">processors</see></param>
        /// <param name="logger"></param>
        public DataPointProcessorService(IDataPointsProcessors processors, ILogger logger)
        {
            _processors = processors;
            _logger = logger;
        }

        /// <inheritdoc/>
        public override async Task<Empty> Process(IAsyncStreamReader<DataPointMessage> requestStream, ServerCallContext context)
        {
            while (await requestStream.MoveNext())
            {
                _logger.Information($"Data Point received");
                try
                {
                    var processor = _processors.GetById(requestStream.Current.Id.To<DataPointProcessorId>());
                    var dataPoint = requestStream.Current.DataPoint;

                    var dataPointInstance = Convert(dataPoint);

                    await processor.Invoke(new TimeSeriesMetadata(Guid.NewGuid()), dataPointInstance);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, "Couldn't process data point");
                }
            }

            return new Empty();
        }

        object Convert(DataTypes.Protobuf.DataPoint dataPoint)
        {
            System.Type valueType = typeof(object);
            object valueInstance = null;
            switch (dataPoint.Value.ValueCase)
            {
                case Value.ValueOneofCase.MeasurementValue:
                    {
                        switch (dataPoint.Value.MeasurementValue.ValueCase)
                        {
                            case Measurement.ValueOneofCase.FloatValue:
                                {
                                    valueType = typeof(Measurement<float>);
                                    valueInstance = dataPoint.Value.ToMeasurement<float>();
                                }
                                break;
                            case Measurement.ValueOneofCase.DoubleValue:
                                {
                                    valueType = typeof(Measurement<double>);
                                    valueInstance = dataPoint.Value.ToMeasurement<double>();
                                }
                                break;
                            case Measurement.ValueOneofCase.Int32Value:
                                {
                                    valueType = typeof(Measurement<int>);
                                    valueInstance = dataPoint.Value.ToMeasurement<int>();
                                }
                                break;
                            case Measurement.ValueOneofCase.Int64Value:
                                {
                                    valueType = typeof(Measurement<Int64>);
                                    valueInstance = dataPoint.Value.ToMeasurement<Int64>();
                                }
                                break;
                        }
                    }
                    break;

                case Value.ValueOneofCase.Vector2Value:
                    valueType = typeof(Vector2);
                    valueInstance = dataPoint.Value.ToVector2();
                    break;

                case Value.ValueOneofCase.Vector3Value:
                    valueType = typeof(Vector3);
                    valueInstance = dataPoint.Value.ToVector3();
                    break;
            }
            var dataPointType = typeof(DataPoint<>).MakeGenericType(new [] { valueType });
            var dataPointInstance = Activator.CreateInstance(dataPointType);
            var valueProperty = dataPointType.GetProperty("Value", BindingFlags.Instance | BindingFlags.Public);
            valueProperty.SetValue(dataPointInstance, valueInstance);

            var timestamp = (Timestamp)dataPoint.Timestamp.ToDateTimeOffset();
            var timestampProperty = dataPointType.GetProperty("Timestamp", BindingFlags.Instance | BindingFlags.Public);
            timestampProperty.SetValue(dataPointInstance, timestamp);

            var timeSeriesProperty = dataPointType.GetProperty("TimeSeries", BindingFlags.Instance | BindingFlags.Public);
            timeSeriesProperty.SetValue(dataPointInstance, dataPoint.TimeSeries.To<TimeSeriesId>());

            return dataPointInstance;
        }
    }
}