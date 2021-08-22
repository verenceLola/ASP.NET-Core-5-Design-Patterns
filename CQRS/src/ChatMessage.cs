using System;
using CQRS.Interfaces;

namespace CQRS
{
    public class ChatMessage
    {
        public ChatMessage(IParticipant sender, string message)
        {
            Sender = sender ?? throw new ArgumentNullException(nameof(sender));
            Message = message ?? throw new ArgumentNullException(nameof(message));
            Date = DateTime.UtcNow;
        }
        public DateTime Date { get; }
        public IParticipant Sender { get; }
        public string Message { get; }
    }
}
