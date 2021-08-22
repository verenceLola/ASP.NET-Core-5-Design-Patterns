using System;

namespace CQRS.Commands
{
    public class JoinChatRoom
    {
        public class Command : Interfaces.ICommand
        {
            public Command(Interfaces.IChatRoom chatRoom, Interfaces.IParticipant requester)
            {
                ChatRoom = chatRoom ?? throw new ArgumentNullException(nameof(chatRoom));
                Requester = requester ?? throw new ArgumentNullException(nameof(requester));
            }
            public Interfaces.IChatRoom ChatRoom { get; }
            public Interfaces.IParticipant Requester { get; }
        }
        public class Handler : Interfaces.ICommandHandler<Command>
        {
            public void Handle(Command command)
            {
                command.ChatRoom.Add(command.Requester);
            }
        }
    }
}
