/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq;
using System.Threading.Tasks;
using Dolittle.Logging;
using Dolittle.TimeSeries.Connectors;
using Dolittle.TimeSeries.DataPoints;
using Dolittle.TimeSeries.DataTypes;

namespace PullConnector
{
    public class MyConnector : IAmAStreamingConnector
    {
        readonly ILogger _logger;
        readonly Random _random;

        public MyConnector(ILogger logger)
        {
            _logger = logger;
            _random = new Random();
        }

        public Source Name => "MyStreamConnector";

        public async Task Connect(StreamConnectorConfiguration configuration, IStreamWriter writer)
        {
            await Task.Run(async () =>
            {
                for (; ; )
                {
                    _logger.Information($"Streaming tags '{string.Join(", ", configuration.Tags.Select(_ => _.Value))}'");
                    var dataPoints = configuration.Tags.Select(_ => new TagDataPoint
                    {
                        Tag = _,
                        Value = (Measurement<float>)_random.NextDouble()
                    });
                    await writer.Write(dataPoints);
                    
                    await Task.Delay(1000);
                }
            });
        }
    }
}