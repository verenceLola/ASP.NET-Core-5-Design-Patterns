using Xunit;
using CQRS;
using CQRS.Interfaces;
using System.Collections.Generic;
using CQRS.Commands;
using CQRS.Queries;

namespace tests
{
    public class ChatRoomTest
    {
        private readonly IMediator _mediator = new Mediator();
        private readonly TestMessageWriter _reaganMessageWriter = new TestMessageWriter();
        private readonly TestMessageWriter _garnerMessageWriter = new TestMessageWriter();
        private readonly TestMessageWriter _cornelaiMessageWriter = new TestMessageWriter();

        private readonly IChatRoom _room1 = new ChatRoom("Room 1");
        private readonly IChatRoom _room2 = new ChatRoom("Room 2");

        private readonly IParticipant _reagan, _garner, _cornelia;

        public ChatRoomTest()
        {
            _mediator.Register(new JoinChatRoom.Handler());
            _mediator.Register(new LeaveChatRoom.Handler());
            _mediator.Register(new SendChatMessage.Handler());
            _mediator.Register(new ListMessages.Handler());
            _mediator.Register(new ListParticipants.Handler());

            _reagan = new Participant(_mediator, "Reagan", _reaganMessageWriter);
            _garner = new Participant(_mediator, "Garner", _garnerMessageWriter);
            _cornelia = new Participant(_mediator, "Cornelia", _cornelaiMessageWriter);
        }
        [Fact]
        public void A_participant_should_be_able_to_list_the_participants_that_joined_a_chatroom()
        {
            _reagan.Join(_room1);
            _reagan.Join(_room2);
            _garner.Join(_room1);
            _cornelia.Join(_room2);

            var room1Participants = _reagan.ListParticipantsOf(_room1);
            var room2Participants = _reagan.ListParticipantsOf(_room2);

            Assert.Collection(room1Participants,
            p => Assert.Same(_reagan, p),
            p => Assert.Same(_garner, p));

            Assert.Collection(room2Participants,
            p => Assert.Same(_reagan, p),
            p => Assert.Same(_cornelia, p));
        }
        [Fact]
        public void A_participant_should_receive_new_messages()
        {
            _reagan.Join(_room1);
            _garner.Join(_room1);
            _garner.Join(_room2);
            _reagan.SendMessageTo(_room1, "Hello!");

            Assert.Collection(_garnerMessageWriter.Output,
            line =>
            {
                Assert.Equal(_room1, line.chatRoom);
                Assert.Equal(_reagan, line.message.Sender);
                Assert.Equal("Hello!", line.message.Message);
            });
        }
        private class TestMessageWriter : IMessageWriter
        {
            public List<(IChatRoom chatRoom, ChatMessage message)> Output { get; } = new List<(IChatRoom, ChatMessage)>();

            public void Write(IChatRoom chatRoom, ChatMessage message)
            {
                Output.Add((chatRoom, message));
            }
        }
    }

}
