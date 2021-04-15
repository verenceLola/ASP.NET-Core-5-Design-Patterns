using System.Collections.Generic;
using Xunit;
using AbstractFactory;
using System;
using System.Collections;

namespace AbstractFactoryTest
{
    public abstract class AbstractFactoryBaseTestData : IEnumerable<object[]>
    {
        private readonly TheoryData<IVehicleFactory, Type> _data = new TheoryData<IVehicleFactory, Type>();
        protected void AddTestData<TConcreteFactory, TExpectedVehicle>() where TConcreteFactory : IVehicleFactory, new()
        {
            _data.Add(new TConcreteFactory(), typeof(TExpectedVehicle));
        }
        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => (IEnumerator)GetEnumerator();
    }
}
