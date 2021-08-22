using System;

namespace Mediator
{
    public interface IMediator
    {
        void Send(Message message);
    }
    public interface IColleague
    {
        string Name { get; }
        void ReceiveMessage(Message message);
    }
    public class Message
    {
        public Message(IColleague from, string content)
        {
            Sender = from ?? throw new ArgumentNullException(nameof(from));
            Conent = content ?? throw new ArgumentNullException(nameof(content));
        }
        public IColleague Sender { get; }
        public string Conent { get; }
    }
    public interface IMessageWriter<TMessage>
    {
        void Write(TMessage message);
    }
}
