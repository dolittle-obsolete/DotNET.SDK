/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Threading.Tasks;
using Dolittle.Logging;
using Dolittle.TimeSeries.DataPoints;
using Dolittle.TimeSeries.DataTypes;

namespace InfluxDBInserter
{
    public class Processor : ICanProcessDataPoints
    {
        readonly ILogger _logger;

        public Processor(ILogger logger)
        {
            _logger = logger;
        }

        [DataPointProcessor]
        public async Task Process(DataPoint<Measurement<float>> dataPoint)
        {
            _logger.Information($"DataPoint received for '{dataPoint.TimeSeries}' with value '{dataPoint.Value.Value}' generated @ '{dataPoint.Timestamp}'");

            // Save to Influx, columns:
            // - TimeSeriesID : Unique Identifier of the TimeSeries
            // - Value.Value
            // - Value.Error
            // - TimeStamp

            // var milliseconds = dataPoint.Timestamp.Value.ToUnixTimeMilliseconds();

            await Task.CompletedTask;
        }

        [DataPointProcessor]
        public async Task Process(DataPoint<Measurement<double>> dataPoint)
        {
            _logger.Information($"DataPoint received for '{dataPoint.TimeSeries}' with value '{dataPoint.Value.Value}' generated @ '{dataPoint.Timestamp}'");

            

            // Save to Influx

            await Task.CompletedTask;
        }        

        [DataPointProcessor]
        public async Task Process(DataPoint<Measurement<int>> dataPoint)
        {
            _logger.Information($"DataPoint received for '{dataPoint.TimeSeries}' with value '{dataPoint.Value.Value}' generated @ '{dataPoint.Timestamp}'");

            // Save to Influx

            await Task.CompletedTask;
        }        

        [DataPointProcessor]
        public async Task Process(DataPoint<Measurement<Vector2>> dataPoint)
        {
            _logger.Information($"DataPoint received for '{dataPoint.TimeSeries}' with value '{dataPoint.Value.Value}' generated @ '{dataPoint.Timestamp}'");

            // Save to Influx

            await Task.CompletedTask;
        }        

        [DataPointProcessor]
        public async Task Process(DataPoint<Measurement<Vector3>> dataPoint)
        {
            _logger.Information($"DataPoint received for '{dataPoint.TimeSeries}' with value '{dataPoint.Value.Value}' generated @ '{dataPoint.Timestamp}'");

            // Save to Influx

            

            await Task.CompletedTask;
        }        
    }
}
