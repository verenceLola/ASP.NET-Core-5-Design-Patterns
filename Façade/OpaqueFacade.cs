using System;
using System.Text;

namespace Façade
{
    public interface IOPaqueFacade
    {
        string ExecuteOperationA();
        string ExecuteOperationB();
    }

    internal class ComponentA
    {
        public string OperationA() => "Component A, Operation A";
        public string OperationB() => "Component A, Operation B";
    }
    internal class ComponentB
    {
        public string OperationC() => "Component B, Operation C";
        public string OperationD() => "Component B, Operation D";
    }
    internal class ComponentC
    {
        public string OperationE() => "Component C, Operation E";
        public string OperationF() => "Component C, Operation F";
    }
    public class OpaqueFacade : IOPaqueFacade
    {
        private readonly ComponentA _componentA;
        private readonly ComponentB _componentB;
        private readonly ComponentC _componentC;

        internal OpaqueFacade(ComponentA componentA, ComponentB componentB, ComponentC componentC)
        {
            _componentA = componentA ?? throw new ArgumentNullException(nameof(componentA));
            _componentB = componentB ?? throw new ArgumentNullException(nameof(componentB));
            _componentC = componentC ?? throw new ArgumentNullException(nameof(componentC));
        }
        public string ExecuteOperationA() => new StringBuilder()
            .AppendLine(_componentA.OperationA())
            .AppendLine(_componentA.OperationB())
            .AppendLine(_componentB.OperationD())
            .AppendLine(_componentC.OperationE())
            .ToString();

        public string ExecuteOperationB() => new StringBuilder()
            .AppendLine(_componentB.OperationC())
            .AppendLine(_componentC.OperationF())
            .ToString();
    }
}
