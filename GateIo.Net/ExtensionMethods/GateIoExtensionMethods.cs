using System;
using System.Collections.Generic;
using System.Linq;

namespace GateIo.Net.ExtensionMethods
{
    /// <summary>
    /// Extension methods specific to using the Gate.io API
    /// </summary>
    public static class GateIoExtensionMethods
    {
        /// <summary>
        /// Get the weight left from the response headers
        /// </summary>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static int? GateIoWeightLeft(this KeyValuePair<string, string[]>[]? headers)
        {
            if (headers == null)
                return null;

            var headerValues = headers.FirstOrDefault(s => s.Key.Equals("X-Gate-RateLimit-Requests-Remain", StringComparison.InvariantCultureIgnoreCase)).Value;
            if (headerValues != null && int.TryParse(headerValues.First(), out var value))
                return value;
            return null;
        }

        /// <summary>
        /// Get the weight limit from the response headers
        /// </summary>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static int? GateIoWeightLimit(this KeyValuePair<string, string[]>[]? headers)
        {
            if (headers == null)
                return null;

            var headerValues = headers.FirstOrDefault(s => s.Key.Equals("X-Gate-RateLimit-Limit", StringComparison.InvariantCultureIgnoreCase)).Value;
            if (headerValues != null && int.TryParse(headerValues.First(), out var value))
                return value;
            return null;
        }
    }
}
