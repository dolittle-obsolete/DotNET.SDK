/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Concepts;

namespace Dolittle.TimeSeries
{
    /// <summary>
    /// Represents the origin of a <see cref="DataPoint{T}"/>
    /// </summary>
    public class Origin : ConceptAs<string>
    {
        /// <summary>
        /// Implicitly convert from a <see cref="string"/> representation of origin to <see cref="Origin"/>
        /// </summary>
        /// <param name="origin"><see cref="string"/> to convert from</param>
        public static implicit operator Origin(string origin) => new Origin { Value = origin };
    }
}