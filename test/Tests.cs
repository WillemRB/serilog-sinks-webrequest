using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Serilog.Sinks.WebRequest.Tests
{
    [TestClass]
    public class Tests
    {
        private string Url = "http://localhost";

        private ILogger logger;

        [TestInitialize]
        public void Initialize()
        {
            logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.WebRequest(new Uri(Url))
                .CreateLogger();
        }

        [TestMethod]
        public void TestMethod()
        {
            logger.Information("Hello World");
        }
    }
}
