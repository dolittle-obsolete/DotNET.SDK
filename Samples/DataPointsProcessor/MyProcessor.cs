/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Threading.Tasks;
using Dolittle.TimeSeries;
using Dolittle.TimeSeries.DataPoints;

namespace PullConnector
{
    public class MyProcessor : ICanProcessDataPoint<float>
    {
        public DataPointsOf<float> Filter => DataPoints.Of<float>().OriginatingFrom("SmartRope");

        public async Task Process(DataPoint<float> dataPoint)
        {
            await Task.CompletedTask;
        }
    }
}