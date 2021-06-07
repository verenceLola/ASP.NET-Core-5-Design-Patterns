using System;

namespace Composite
{
    class Program
    {
        static void Main(string[] args)
        {
            var compositeDataStructure = new DefaultCorpoationFactory().Create();

            Console.WriteLine(compositeDataStructure.Display());
        }
    }
}
