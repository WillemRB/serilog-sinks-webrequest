using System;
using System.Collections.Specialized;
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
        /// Collection containing additional headers that are added to the request.
        /// </summary>
        /// <remarks>
        /// This can be used if the endpoint that receives the request uses, or requires, extra
        /// headers to be added to the request.
        /// </remarks>
        public NameValueCollection Headers { get; private set; }

        /// <summary>
        /// Create a new WebRequestSink.
        /// </summary>
        /// <param name="uri">The Uri to send the request to</param>
        /// <param name="httpMethod">The HTTP Method used for the request</param>
        /// <param name="contentType">The content type header value for the request</param>
        /// <param name="headers">Additional headers that are added to the request</param>
        /// <remarks>
        /// <para>Common content types: 'text/plain', 'application/json'</para>
        /// </remarks>
        public WebRequestSink(Uri uri, string httpMethod = "POST", string contentType = "text/plain", NameValueCollection headers = null)
        {
            Uri = uri;
            HttpMethod = httpMethod;
            ContentType = contentType;
            Headers = headers ?? new NameValueCollection();
        }

        public void Emit(LogEvent logEvent)
        {
            var body = logEvent.RenderMessage();

            var request = System.Net.WebRequest.Create(Uri);
            request.Method = HttpMethod;
            request.ContentType = ContentType;

            request.Headers.Add(Headers);

            using (var stream = request.GetRequestStream())
            {
                var bytes = Encoding.UTF8.GetBytes(body);
                stream.Write(bytes, 0, bytes.Length);
            }

            var response = request.GetResponse();

            return;
        }
    }
}
