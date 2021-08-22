namespace CQRS.Interfaces {
    public interface IMessageWriter
    {
        void Write(IChatRoom chatRoom, ChatMessage message);
    }
}
