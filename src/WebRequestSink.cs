using System;
using System.Text;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Sinks.WebRequest
{
    public class WebRequestSink : ILogEventSink
    {
        public Uri Uri { get; private set; }

        public string HttpMethod { get; private set; }

        public string ContentType { get; private set; }

        /// <summary>
        /// Create a new WebRequestSink.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="httpMethod"></param>
        /// <param name="contentType"></param>
        /// <remarks>
        ///     <para>
        ///         Common content types: 'text/plain', 'application/json'
        ///     </para>
        /// </remarks>
        public WebRequestSink(Uri uri, string httpMethod = "POST", string contentType = "text/plain")
        {
            Uri = uri;
            HttpMethod = httpMethod;
            ContentType = contentType;
        }

        public void Emit(LogEvent logEvent)
        {
            var body = logEvent.RenderMessage();

            var request = System.Net.WebRequest.Create(Uri);
            request.Method = HttpMethod;
            request.ContentType = ContentType;

            using (var stream = request.GetRequestStream())
            {
                var bytes = Encoding.UTF8.GetBytes(body);
                stream.Write(bytes, 0, bytes.Length);
            }

            var response = request.GetResponse();
        }
    }
}
