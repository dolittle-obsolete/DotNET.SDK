/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq;
using System.Threading.Tasks;
using Dolittle.Logging;
using Dolittle.TimeSeries;
using Dolittle.TimeSeries.Connectors;

namespace PullConnector
{
    public class MyConnector : IAmAStreamingConnector
    {
        public event TagDataReceived DataReceived = (d) => Task.CompletedTask;

        readonly ILogger _logger;
        readonly Random _random;

        public MyConnector(ILogger logger)
        {
            _logger = logger;
            _random = new Random();
        }

        public Source Name => "MyStreamConnector";

        public async Task Connect(StreamConnectorConfiguration configuration)
        {
            await Task.Run(async () =>
            {
                for (; ; )
                {
                    _logger.Information($"Streaming tags '{string.Join(", ", configuration.Tags.Select(_ => _.Value))}'");
                    var dataPoints = configuration.Tags.Select(_ => new TagDataPoint
                    {
                        Tag = _,
                        Value = (float)_random.NextDouble()
                    });
                    
                    foreach( var dataPoint in dataPoints )
                    {
                        await DataReceived(dataPoint);

                        await Task.Delay(100);
                    }
                }
            });
        }
    }
}