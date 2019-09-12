/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dolittle.Logging;
using Dolittle.TimeSeries;
using Dolittle.TimeSeries.Connectors;

namespace PullConnector
{
    public class MyConnector : IAmAPullConnector
    {
        readonly ILogger _logger;
        readonly Random _random;

        public MyConnector(ILogger logger)
        {
            _logger = logger;
            _random = new Random();
        }
        
        public Source Name => "MyConnector";

        public Task<IEnumerable<TagDataPoint>> Pull(IEnumerable<Tag> tags)
        {
            _logger.Information($"Pulling tags '{string.Join(", ", tags.Select(_ => _.Value))}'");
            return Task.FromResult(tags.Select(_ => new TagDataPoint {
                Tag = _,
                Value = (float)_random.NextDouble()
            }));
        }
    }
}