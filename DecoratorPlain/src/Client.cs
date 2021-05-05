using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DecoratorPlain
{
    public class Client
    {
        private readonly IComponent _component;
        public Client(IComponent component)
        {
            _component = component ?? throw new ArgumentNullException(nameof(component));

        }
        public Task ExecuteAsync(HttpContext context)
        {
            var result = _component.Operation();

            return context.Response.WriteAsync($"Opertation: {result}");
        }
    }
    public interface IComponent
    {
        string Operation();
    }
    public class ComponentA : IComponent
    {
        public string Operation() => "Gretings from ComponentA";
    }
    public class DecoratorA : IComponent
    {
        private readonly IComponent _component;
        public DecoratorA(IComponent component)
        {
            _component = component ?? throw new ArgumentNullException(nameof(component));
        }
        public string Operation()
        {
            var result = _component.Operation();

            return $"<DecoratorA>{result}</DecortorA>";
        }
    }
    public class DecoratorB : IComponent
    {
        private readonly IComponent _component;
        public DecoratorB(IComponent component)
        {
            _component = component ?? throw new ArgumentNullException(nameof(component));
        }
        public string Operation()
        {
            var result = _component.Operation();

            return $"<DecoratorB>{result}</DecoratorB>";
        }
    }
}
