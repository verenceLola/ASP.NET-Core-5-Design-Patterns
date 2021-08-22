using System;
using System.Collections.Generic;
using CQRS.Interfaces;

namespace CQRS.Queries
{
    public class ListParticipants
    {
        public class Query : IQuery<IEnumerable<IParticipant>>
        {
            public Query(IChatRoom chatRoom, IParticipant requester)
            {
                Requester = requester ?? throw new ArgumentNullException(nameof(requester));
                ChatRoom = chatRoom ?? throw new ArgumentNullException(nameof(chatRoom));
            }
            public IParticipant Requester { get; }
            public IChatRoom ChatRoom { get; }
        }
        public class Handler : IQueryHandler<Query, IEnumerable<IParticipant>>
        {
            public IEnumerable<IParticipant> Handle(Query query) => query.ChatRoom.ListParticipants();
        }
    }
}
