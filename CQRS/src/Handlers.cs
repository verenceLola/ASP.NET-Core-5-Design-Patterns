using System;
using System.Collections.Generic;
using CQRS.Interfaces;

namespace CQRS {
    public class HandlerList
    {
        private readonly List<object> _commandHandlers = new List<object>();
        private readonly List<object> _queryHandlers = new List<object>();

        public void Add<TCommand>(ICommandHandler<TCommand> handler)
            where TCommand : ICommand
        {
            _commandHandlers.Add(handler);
        }

        public void Add<TQuery, TReturn>(IQueryHandler<TQuery, TReturn> handler)
            where TQuery : IQuery<TReturn>
        {
            _queryHandlers.Add(handler);
        }

        public IEnumerable<ICommandHandler<TCommand>> FindAll<TCommand>()
            where TCommand : ICommand
        {
            foreach (var handler in _commandHandlers)
            {
                if (handler is ICommandHandler<TCommand> output)
                {
                    yield return output;
                }
            }
        }
        public IQueryHandler<TQuery, TReturn> Find<TQuery, TReturn>()
            where TQuery : IQuery<TReturn>
        {
            foreach (var handler in _queryHandlers)
            {
                if (handler is IQueryHandler<TQuery, TReturn> output)
                {
                    return output;
                }
            }
            throw new QueryHandlerNotFoundException(typeof(TQuery));
        }
    }

    public class HandlerDictionary
    {
        private readonly Dictionary<Type, HandlerList> _handlers = new Dictionary<Type, HandlerList>();

        public void AddHandler<TCommand>(ICommandHandler<TCommand> handler)
            where TCommand : ICommand
        {
            var type = typeof(TCommand);
            EnforceTypeEntry(type);
            var registeredHandlers = _handlers[type];
            registeredHandlers.Add(handler);
        }


        public void AddHandler<TQuery, TReturn>(IQueryHandler<TQuery, TReturn> handler)
            where TQuery : IQuery<TReturn>
        {
            var type = typeof(TQuery);
            EnforceTypeEntry(type);
            var registeredHandlers = _handlers[type];
            registeredHandlers.Add(handler);
        }

        public IEnumerable<ICommandHandler<TCommand>> FindAll<TCommand>()
            where TCommand : ICommand
        {
            var type = typeof(TCommand);
            EnforceTypeEntry(type);
            var registeredHandlers = _handlers[type];
            return registeredHandlers.FindAll<TCommand>();
        }

        public IQueryHandler<TQuery, TReturn> Find<TQuery, TReturn>()
            where TQuery : IQuery<TReturn>
        {
            var type = typeof(TQuery);
            EnforceTypeEntry(type);
            var registeredHandlers = _handlers[type];
            return registeredHandlers.Find<TQuery, TReturn>();
        }

        private void EnforceTypeEntry(Type type)
        {
            if (!_handlers.ContainsKey(type))
            {
                _handlers.Add(type, new HandlerList());
            }
        }
    }
    public class QueryHandlerNotFoundException : Exception
    {
        public QueryHandlerNotFoundException(Type queryType)
            : base($"No handler found for query '{queryType}'.")
        {
        }
    }
}
