/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Reflection;
using System.Threading.Tasks;
using Machine.Specifications;

namespace Dolittle.TimeSeries.DataPoints.for_DataPointProcessor
{
    public class when_creating_with_valid_method
    {
        class Processor : ICanProcessDataPoints
        {
            Task MyProcessorMethod()
            {
                return Task.CompletedTask;
            }
        }

        static Processor instance;
        static MethodInfo method;
        static DataPointProcessor result;

        Establish context = () =>
        {
            instance = new Processor();
            method = typeof(Processor).GetMethod("MyProcessorMethod", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        };

        Because of = () => result = new DataPointProcessor(instance, method);

        It set_instance_on_processor = () => result.Processor.ShouldEqual(instance);
        It set_method_on_processor = () => result.Method.ShouldEqual(method);
    }

}