using CQRS.Interfaces;

namespace CQRS
{
    public class Mediator : Interfaces.IMediator
    {
        private readonly HandlerDictionary _handlers = new HandlerDictionary();
        public void Register<TCommand>(ICommandHandler<TCommand> commandHandler) where TCommand : ICommand
        {
            _handlers.AddHandler(commandHandler);
        }
        public void Register<TQuery, TReturn>(IQueryHandler<TQuery, TReturn> commandHandler) where TQuery : IQuery<TReturn>
        {
            _handlers.AddHandler(commandHandler);
        }
        public TReturn Send<TQuery, TReturn>(TQuery query) where TQuery : IQuery<TReturn>
        {
            var handler = _handlers.Find<TQuery, TReturn>();

            return handler.Handle(query);
        }
        public void Send<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handlers = _handlers.FindAll<TCommand>();

            foreach (var hander in handlers)
            {
                hander.Handle(command);
            }
        }
    }
}
