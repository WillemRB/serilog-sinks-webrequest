using System;
using Serilog.Configuration;

namespace Serilog.Sinks.WebRequest
{
    public static class LoggerConfigurationExtensions
    {
        /// <summary>
        /// Writes log events to a HTTP endpoint.
        /// </summary>
        /// <param name="config">Logger sink configuration</param>
        /// <param name="uri">The Uri to send the request to</param>
        /// <param name="httpMethod">The HTTP Method used for the request</param>
        /// <param name="contentType">The content type header value for the request</param>
        /// <returns></returns>
        public static LoggerConfiguration WebRequest(
            this LoggerSinkConfiguration sinkConfiguration,
            Uri uri,
            string httpMethod = "POST",
            string contentType = "plain/text")
        {
            return sinkConfiguration.Sink(new WebRequestSink(uri, httpMethod, contentType));
        }
    }
}
