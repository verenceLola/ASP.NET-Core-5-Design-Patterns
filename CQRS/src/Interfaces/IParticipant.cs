using System.Collections.Generic;

namespace CQRS.Interfaces
{
    public interface IParticipant
    {
        string Name { get; }
        void Join(IChatRoom chatRoom);
        void Leave(IChatRoom chatRoom);
        void SendMessageTo(IChatRoom chatRoom, string message);
        void NewMessageReceivedFrom(IChatRoom chatRoom, ChatMessage message);
        IEnumerable<IParticipant> ListParticipantsOf(IChatRoom chatRoom);
        IEnumerable<ChatMessage> ListMessagesOf(IChatRoom chatRoom);
    }
}
