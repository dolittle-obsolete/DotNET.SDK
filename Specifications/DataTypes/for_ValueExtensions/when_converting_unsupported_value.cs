/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Machine.Specifications;

namespace Dolittle.TimeSeries.DataTypes.for_ValueExtensions
{
    public class when_converting_unsupported_value 
    {
        class MyValue : IValue {}

        static Exception result;

        Because of = () => result = Catch.Exception(() => new MyValue().ToProtobuf());

        It should_throw_unsupported_value_type = () => result.ShouldBeOfExactType<UnsupportedValueType>();
    }
}