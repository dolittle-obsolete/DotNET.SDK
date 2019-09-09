/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Threading.Tasks;
using Dolittle.Clients;
using Dolittle.TimeSeries;
using Dolittle.TimeSeries.Connectors;

namespace PullConnector
{
    public class MyConnector : IAmAPullConnector
    {
        public Source Name => "MyConnector";

        public IEnumerable<TagWithData> GetAllData()
        {
            throw new System.NotImplementedException();
        }

        public object GetData(Tag tag)
        {
            throw new System.NotImplementedException();
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