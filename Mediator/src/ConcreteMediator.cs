using System;
using System.Collections.Generic;

namespace Mediator
{
    public class ConcreteMediator : IMediator
    {
        private readonly List<IColleague> _colleagues;
        public ConcreteMediator(params IColleague[] colleagues)
        {
            _colleagues = new List<IColleague>(colleagues) ?? throw new ArgumentNullException(nameof(colleagues));
        }
        public void Send(Message message)
        {
            foreach (var colleague in _colleagues)
            {
                colleague.ReceiveMessage(message);
            }
        }
    }
}
