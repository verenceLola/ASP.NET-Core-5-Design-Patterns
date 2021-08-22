using System;
using CQRS.Interfaces;


namespace CQRS.Commands
{
    public class LeaveChatRoom{
        public class Command : ICommand
        {
            public Command(IChatRoom chatRoom, IParticipant requester)
            {
                ChatRoom = chatRoom ?? throw new ArgumentNullException(nameof(chatRoom));
                Requester = requester ?? throw new ArgumentNullException(nameof(requester));
            }
            public IChatRoom ChatRoom { get; }
            public IParticipant Requester { get; }
        }
        public class Handler : ICommandHandler<Command>
        {
            public void Handle(Command command)
            {
                command.ChatRoom.Remove(command.Requester);
            }
        }
    }
}
