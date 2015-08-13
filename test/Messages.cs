namespace Serilog.Sinks.WebRequest.Tests
{
    public class GlipMessage
    {
        public string Icon { get; set; }

        public string Activity { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public GlipMessage()
        {
            Icon = @"http://tinyurl.com/pn46fgp";
            Activity = "Running tests";
            Title = "This is a title";
            Body = "This is the default test body of this message.";
        }
    }
}
