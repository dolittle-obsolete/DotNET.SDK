/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Reflection;
using System.Threading.Tasks;
using Dolittle.TimeSeries.DataTypes;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.TimeSeries.DataPoints.for_DataPointProcessor
{
    public class when_invoking_processor_that_takes_data_point_and_time_series_metadata
    {
        public interface Processor : ICanProcessDataPoints
        {
            Task MyProcessorMethod(DataPoint<Measurement<float>> dataPoint, TimeSeriesMetadata metadata);
        }

        static Mock<Processor> instance;
        static MethodInfo method;
        static DataPointProcessor processor;

        static DataPoint<Measurement<float>> data_point;
        static TimeSeriesMetadata metadata;


        Establish context = () =>
        {
            instance = new Mock<Processor>();
            method = typeof(Processor).GetMethod("MyProcessorMethod", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            processor = new DataPointProcessor(instance.Object, method);
            data_point = new DataPoint<Measurement<float>> {
                Value = new Measurement<float> { Value = 42f, Error = 43f }
            };
            metadata = new TimeSeriesMetadata(Guid.NewGuid());
        };

        Because of = () => processor.Invoke(metadata,data_point);

        It should_invoke_the_processor_with_the_data_point = () => instance.Verify(_ => _.MyProcessorMethod(data_point, metadata), Moq.Times.Once());
    }    
}