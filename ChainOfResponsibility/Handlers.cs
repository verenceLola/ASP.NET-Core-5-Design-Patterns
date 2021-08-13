using System;

namespace ChainOfResponsibility
{
    public class AlarmTriggeredHandler : MessageHandlerBase
    {
        private readonly IMessageHandler _next;
        protected override string HandledMessageName => "AlarmTriggered";
        public AlarmTriggeredHandler(IMessageHandler next = null) : base(next) { }

        protected override void Process(Message message)
        {
            //  Do something clever
        }
    }

    public class AlarmPausedHandler : MessageHandlerBase
    {
        private readonly IMessageHandler _next;
        protected override string HandledMessageName => "AlarmPaused";
        public AlarmPausedHandler(IMessageHandler next = null) : base(next) { }

        protected override void Process(Message message)
        {
            // Do somthing clever
        }
    }

    public class AlarmStoppedHandler : MessageHandlerBase
    {
        private readonly IMessageHandler _next;
        protected override string HandledMessageName => "AlarmStopped";
        public AlarmStoppedHandler(IMessageHandler next = null) : base(next) { }

        protected override void Process(Message message)
        {
            // Do somthing clever
        }
    }

    public class DefaultHandler : IMessageHandler
    {
        public void Handle(Message message)
        {
            throw new NotSupportedException($"Message named '{message.Name}' are not supported");
        }
    }
}
