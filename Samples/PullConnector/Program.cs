/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Threading.Tasks;
using Dolittle.Clients;
using Dolittle.Logging;
using Dolittle.TimeSeries;
using Dolittle.TimeSeries.Connectors;

namespace PullConnector
{
    public class MyConnector : IAmAPullConnector
    {
        readonly ILogger _logger;

        public MyConnector(ILogger logger)
        {
            _logger = logger;
        }
        
        public Source Name => "MyConnector";

        public IEnumerable<TagWithData> Pull(IEnumerable<Tag> tags)
        {
            _logger.Information("Pulling tag data");
            return null;
        }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            await Bootloader.Start();
        }
    }
}