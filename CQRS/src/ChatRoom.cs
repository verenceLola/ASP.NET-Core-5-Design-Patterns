using System;
using System.Collections.Generic;
using CQRS.Interfaces;

namespace CQRS
{
    public class ChatRoom : IChatRoom
    {
        private readonly List<IParticipant> _participants = new List<IParticipant>();
        private readonly List<ChatMessage> _chatMessages = new List<ChatMessage>();
        public ChatRoom(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
        public string Name { get; }
        public void Add(IParticipant participant) => _participants.Add(participant);
        public void Remove(IParticipant participant) => _participants.Remove(participant);
        public IEnumerable<IParticipant> ListParticipants() => _participants.AsReadOnly();
        public void Add(ChatMessage message) => _chatMessages.Add(message);
        public IEnumerable<ChatMessage> ListMessages() => _chatMessages.AsReadOnly();
    }
}
