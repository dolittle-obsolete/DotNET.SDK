/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Threading.Tasks;
using Dolittle.Heads;
using Dolittle.Protobuf;
using Dolittle.TimeSeries.DataTypes;
using Dolittle.TimeSeries.DataTypes.Runtime;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using static Dolittle.TimeSeries.DataPoints.Runtime.DataPointStream;

namespace Dolittle.TimeSeries.DataPoints
{
    /// <summary>
    /// Represents an implementation of <see cref="IDataPointPublisher"/>
    /// </summary>
    public class DataPointPublisher : IDataPointPublisher
    {
        readonly AsyncClientStreamingCall<DataPoint, Empty>  _streamCall;

        /// <summary>
        /// Initializes a new instance of <see cref="DataPointPublisher"/>
        /// </summary>
        /// <param name="client"><see cref="IClientFor{T}"/> <see cref="DataPointStreamClient"/></param>
        public DataPointPublisher(IClientFor<DataPointStreamClient> client)
        {
            _streamCall = client.Instance.Open();
        }


        /// <inheritdoc/>
        public async Task Publish<TValue>(DataPoint<TValue> dataPoint) where TValue:IValue
        {
            var converted = new DataPoint
            {
                TimeSeries = dataPoint.TimeSeries?.ToProtobuf() ?? Guid.Empty.ToProtobuf(),
                Value = dataPoint.Value.ToProtobuf(),
                Timestamp = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTimeOffset(dataPoint.Timestamp)
            };
            await _streamCall.RequestStream.WriteAsync(converted);
        }
    }
}