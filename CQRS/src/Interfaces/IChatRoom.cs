using System.Collections.Generic;

namespace CQRS.Interfaces
{
    public interface IChatRoom
    {
        string Name { get; }
        void Add(IParticipant participant);
        void Remove(IParticipant participant);
        IEnumerable<IParticipant> ListParticipants();
        void Add(ChatMessage message);
        IEnumerable<ChatMessage> ListMessages();
    }
}
