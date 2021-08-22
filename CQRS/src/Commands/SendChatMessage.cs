using System;
using CQRS.Interfaces;

namespace CQRS.Commands
{
    public class SendChatMessage
    {
        public class Command : ICommand
        {
            public Command(IChatRoom chatRoom, ChatMessage message)
            {
                ChatRoom = chatRoom ?? throw new ArgumentNullException(nameof(chatRoom));
                Message = message ?? throw new ArgumentNullException(nameof(message));
            }
            public IChatRoom ChatRoom { get; }
            public ChatMessage Message { get; }
        }
        public class Handler : ICommandHandler<Command>
        {
            public void Handle(Command command)
            {
                command.ChatRoom.Add(command.Message);

                foreach (var participant in command.ChatRoom.ListParticipants())
                {
                    participant.NewMessageReceivedFrom(command.ChatRoom, command.Message);
                }
            }
        }
    }
}
