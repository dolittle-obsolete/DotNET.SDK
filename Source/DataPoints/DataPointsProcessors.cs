/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Dolittle.Clients;
using Dolittle.Collections;
using Dolittle.Lifecycle;
using Dolittle.Logging;
using Dolittle.Protobuf;
using Dolittle.TimeSeries.DataTypes;
using Dolittle.TimeSeries.DataTypes.Runtime;
using Dolittle.Types;
using Grpc.Core;
using static Dolittle.TimeSeries.DataPoints.Runtime.DataPointProcessors;
using grpc = Dolittle.TimeSeries.DataPoints.Runtime;

namespace Dolittle.TimeSeries.DataPoints
{
    /// <summary>
    /// Represents an implementation of <see cref="IDataPointsProcessors"/>
    /// </summary>
    [Singleton]
    public class DataPointsProcessors : IDataPointsProcessors
    {
        readonly IInstancesOf<ICanProcessDataPoints> _processors;
        readonly ConcurrentDictionary<DataPointProcessorId, DataPointProcessor> _dataProcessors = new ConcurrentDictionary<DataPointProcessorId, DataPointProcessor>();
        readonly ILogger _logger;
        readonly IClientFor<DataPointProcessorsClient> _client;

        /// <summary>
        /// Initializes a new instance of <see cref="DataPointsProcessors"/>
        /// </summary>
        /// <param name="processors">Instances of <see cref="ICanProcessDataPoints">processors</see></param>
        /// <param name="client"><see cref="IClientFor{T}">Client</see> for <see cref="DataPointProcessorsClient"/></param>
        /// <param name="logger"><see cref="ILogger"/> for logging</param>
        public DataPointsProcessors(
            IInstancesOf<ICanProcessDataPoints> processors,
            IClientFor<DataPointProcessorsClient> client,
            ILogger logger)
        {
            _processors = processors;
            _logger = logger;
            _client = client;
        }

        void Discover()
        {
            _processors.ForEach(_ =>
            {
                var methods = _.GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                var processorMethods = methods.Where(method => method.GetCustomAttributes().Any(attribute => attribute is DataPointProcessorAttribute));
                if (processorMethods.Count() == 0)
                {
                    _logger.Warning($"DataPoint processor of type '{_.GetType().AssemblyQualifiedName}' does not seem to have any methods adorned with [DataPointProcessor] - this means it does not have any processors");
                }
                else
                {
                    processorMethods.ForEach(method =>
                    {
                        var processor = new DataPointProcessor(_, method);
                        _dataProcessors[processor.Id] = processor;
                    });
                }
            });
        }

        /// <inheritdoc/>
        public void Start()
        {
            Discover();

            _dataProcessors.Values.ForEach(_ =>
            {
                var streamingCall = _client.Instance.Open(new grpc.DataPointProcessor
                {
                    Id = _.Id.ToProtobuf()
                });

                Task.Run(async() => await Process(_, streamingCall.ResponseStream));
            });
        }

        /// <inheritdoc/>
        public DataPointProcessor GetById(DataPointProcessorId id)
        {
            return _dataProcessors[id];
        }

        async Task Process(DataPointProcessor processor, IAsyncStreamReader<DataPoint> streamReader)
        {
            while (await streamReader.MoveNext())
            {
                var dataPoint = streamReader.Current;

                try
                {
                    var dataPointInstance = Convert(dataPoint);
                    await processor.Invoke(
                            new TimeSeriesMetadata(
                                    dataPoint.TimeSeries.To<TimeSeriesId>()),
                                    dataPointInstance);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, "Error processing datapoint");
                }
            }
        }

        object Convert(DataPoint dataPoint)
        {
            System.Type valueType = typeof(object);
            object valueInstance = null;
            switch (dataPoint.Value.ValueCase)
            {
                case Value.ValueOneofCase.MeasurementValue:
                    {
                        valueType = typeof(DataTypes.Measurement);
                        valueInstance = dataPoint.Value.ToMeasurement();
                    }
                    break;

                case Value.ValueOneofCase.Vector2Value:
                    valueType = typeof(DataTypes.Vector2);
                    valueInstance = dataPoint.Value.ToVector2();
                    break;

                case Value.ValueOneofCase.Vector3Value:
                    valueType = typeof(DataTypes.Vector3);
                    valueInstance = dataPoint.Value.ToVector3();
                    break;
            }
            var dataPointType = typeof(DataPoint<>).MakeGenericType(new [] { valueType });
            var dataPointInstance = Activator.CreateInstance(dataPointType);
            var valueProperty = dataPointType.GetProperty("Value", BindingFlags.Instance | BindingFlags.Public);
            valueProperty.SetValue(dataPointInstance, valueInstance);

            var timestamp = (Timestamp) dataPoint.Timestamp.ToDateTimeOffset();
            var timestampProperty = dataPointType.GetProperty("Timestamp", BindingFlags.Instance | BindingFlags.Public);
            timestampProperty.SetValue(dataPointInstance, timestamp);

            var timeSeriesProperty = dataPointType.GetProperty("TimeSeries", BindingFlags.Instance |  BindingFlags.Public);
            timeSeriesProperty.SetValue(dataPointInstance, dataPoint.TimeSeries.To<TimeSeriesId>());

            return dataPointInstance;
        }
    }
}