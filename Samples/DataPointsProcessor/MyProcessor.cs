/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Threading.Tasks;
using Dolittle.Logging;
using Dolittle.TimeSeries;
using Dolittle.TimeSeries.DataPoints;
using Dolittle.TimeSeries.DataTypes;

namespace PullConnector
{
    public class MyProcessor : ICanProcessDataPoints
    {
        readonly IDataPointPublisher _publisher;
        private readonly ILogger _logger;

        public MyProcessor(IDataPointPublisher publisher, ILogger logger)
        {
            _publisher = publisher;
            _logger = logger;
        }

        [DataPointProcessor]
        public async Task Tension(DataPoint<Measurement<float>> dataPoint)
        {
            _logger.Information($"DataPoint received with value '{dataPoint.Value.Value}'");

            await Task.CompletedTask;

            await _publisher.Publish(dataPoint);
        }
    }
}

/*
public DataPointsOf<Vector2> TensionFilter => DataPoints.Of<Vector2>().ForTimeSeries(
                                                                            (TimeSeriesId)"fce5460a-555d-4399-bfcc-1e340cae2501", 
                                                                            (TimeSeriesId)"ee8098ac-eac6-4986-bec3-5d2bda8b63f7");*/