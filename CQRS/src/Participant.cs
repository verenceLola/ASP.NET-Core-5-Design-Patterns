using System;
using System.Collections.Generic;
using CQRS.Interfaces;
using CQRS.Commands;
using CQRS.Queries;

namespace CQRS
{
    public class Participant : IParticipant
    {
        private readonly IMediator _mediator;
        private readonly IMessageWriter _messageWriter;
        public Participant(IMediator mediator, string name, IMessageWriter messageWriter)
        {
            _messageWriter = messageWriter ?? throw new ArgumentNullException(nameof(messageWriter));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
        public string Name { get; }
        public void Join(IChatRoom chatRoom) => _mediator.Send<JoinChatRoom.Command>(
            new JoinChatRoom.Command(chatRoom, this)
        );
        public void Leave(IChatRoom chatRoom) => _mediator.Send(
            new LeaveChatRoom.Command(chatRoom, this)
        );
        public IEnumerable<ChatMessage> ListMessagesOf(IChatRoom chatRoom) => _mediator.Send<ListMessages.Query, IEnumerable<ChatMessage>>(
            new ListMessages.Query(chatRoom, this)
        );
        public void SendMessageTo(IChatRoom chatRoom, string message) =>
            _mediator.Send<SendChatMessage.Command>(new SendChatMessage.Command(
                chatRoom,
                new ChatMessage(this, message)
            ));
        public void NewMessageReceivedFrom(IChatRoom chatRoom, ChatMessage message) =>
            _messageWriter.Write(chatRoom, message);
        public IEnumerable<IParticipant> ListParticipantsOf(IChatRoom chatRoom) =>
            _mediator.Send<ListParticipants.Query, IEnumerable<IParticipant>>(
                new ListParticipants.Query(chatRoom, this)
            );
    }
}
