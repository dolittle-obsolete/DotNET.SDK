/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using Dolittle.Clients;
using Dolittle.Collections;
using Dolittle.Lifecycle;
using Dolittle.Logging;
using Dolittle.Protobuf;
using Dolittle.Types;
using static Dolittle.TimeSeries.Runtime.DataPoints.Grpc.Server.DataPointProcessors;
using grpc = Dolittle.TimeSeries.Runtime.DataPoints.Grpc.Server;

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
        /// 
        /// </summary>
        /// <param name="processors"></param>
        /// <param name="client"></param>
        /// <param name="logger"></param>
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
        public void Register()
        {
            Discover();

            _dataProcessors.Values.ForEach(_ => 
            {
                _client.Instance.Register(new grpc.DataPointProcessor
                {
                    Id = _.Id.ToProtobuf()
                });
            });
        }

        /// <inheritdoc/>
        public DataPointProcessor GetById(DataPointProcessorId id)
        {
            return _dataProcessors[id];
        }
    }
}