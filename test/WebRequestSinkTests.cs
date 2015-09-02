using System;
using System.Collections.Specialized;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Serilog.Sinks.WebRequest.Tests
{
    [TestClass]
    public class WebRequestSinkTests
    {
        private string Url = ConfigurationManager.AppSettings["testUrl"];

        private string glipWebhook = ConfigurationManager.AppSettings["glipWebHook"];

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

            logger.Information("{{ \"icon\": {icon}, \"activity\": {activity}, \"title\": {title}, \"body\": {body} }}",
                message.Icon, message.Activity, message.Title, message.Body);
        }
    }
}
