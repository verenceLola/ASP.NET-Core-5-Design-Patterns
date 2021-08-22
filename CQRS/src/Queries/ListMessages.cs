using System;
using CQRS.Interfaces;
using System.Collections.Generic;

namespace CQRS.Queries
{
    public class ListMessages
    {
        public class Query : IQuery<IEnumerable<ChatMessage>>
        {
            public Query(IChatRoom chatRoom, IParticipant requester)
            {
                Requester = requester ?? throw new ArgumentNullException(nameof(requester));
                ChatRoom = chatRoom ?? throw new ArgumentNullException(nameof(chatRoom));
            }
            public IParticipant Requester { get; }
            public IChatRoom ChatRoom { get; }
        }
        public class Handler : IQueryHandler<Query, IEnumerable<ChatMessage>>
        {
            public IEnumerable<ChatMessage> Handle(Query query) => query.ChatRoom.ListMessages();
        }
    }
}
