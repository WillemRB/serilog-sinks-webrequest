using System;
using System.Collections.Specialized;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Serilog.Sinks.WebRequest.Tests
{
    [TestClass]
    public class WebRequestSinkTests
    {
        private string Url = "http://localhost";

        private string glipWebhook = "https://hooks.glip.com/webhook/{guid}";

        [TestMethod]
        public void LoggerTest()
        {
            var logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.WebRequest(new Uri(Url))
                .CreateLogger();

            logger.Information("Hello World");
        }

        [TestMethod]
        public void AdditionalHeadersTest()
        {
            var headers = new NameValueCollection();
            headers.Add("X-Test-Header", "Test Header Value");

            var logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.WebRequest(new Uri(glipWebhook), contentType: "application/json", headers: headers)
                .CreateLogger();

            var message = new GlipMessage();

            logger.Information("{{ \"icon\": {icon}, \"activity\": {activity}, \"title\": {titile}, \"body\": {body} }}",
                message.Icon, message.Activity, message.Title, message.Body);
        }
    }
}
