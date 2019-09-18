/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Reflection;

namespace Dolittle.TimeSeries.DataPoints
{
    /// <summary>
    /// Exception that gets thrown if a data point processor method is not async
    /// </summary>
    public class DataPointProcessorMethodMustBeAsync : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="DataPointProcessorMethodMustBeAsync"/>
        /// </summary>
        /// <param name="type"><see cref="Type"/> that holds the processor method</param>
        /// <param name="method">The processor <see cref="MethodInfo">method</see></param>
        /// <returns></returns>
        public DataPointProcessorMethodMustBeAsync(Type type, MethodInfo method) 
            : base($"DataProcess method '{method.Name}' on '{type.AssemblyQualifiedName}' does not return a Task. Methods must have an asynchronous signature.")
        {

        }
    }
}