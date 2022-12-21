using System.Diagnostics.Tracing;
using System.Reflection;

namespace EventSourceSampleAnnotations
{
    internal class SampleEventSource : EventSource
    {
        static class Keywords
        {
            public const EventKeywords Network = (EventKeywords)1;
            public const EventKeywords Database = (EventKeywords)2;
            public const EventKeywords Diagnostics = (EventKeywords)3;
            public const EventKeywords Performance = (EventKeywords)4;
        }

        static class Tasks
        {
            public const EventTask CreateMenus = (EventTask)1;
            public const EventTask QueryMenus = (EventTask)2;
        }

        private SampleEventSource() :
            base(Assembly.GetExecutingAssembly().FullName!) { }

        public static SampleEventSource Log = new SampleEventSource();

        [Event(1, Level = EventLevel.Verbose, Opcode = EventOpcode.Start)]
        public void Startup() => WriteEvent(1);

        [Event(2, Keywords = Keywords.Network, Level = EventLevel.Informational, Message = "{0}", Opcode = EventOpcode.Send)]
        public void CallService(string url) => WriteEvent(2, url);

        [Event(3, Keywords = Keywords.Network, Level = EventLevel.Informational, Message = "{0}", Opcode = EventOpcode.Receive)]
        public void CalledService(string url, int length) => WriteEvent(3, url, length);

        [Event(4, Keywords = Keywords.Network, Level = EventLevel.Error, Message = "{0}", Opcode = EventOpcode.Info)]
        public void ServiceError(string message, int error) => WriteEvent(4, message, error);
    }
}
