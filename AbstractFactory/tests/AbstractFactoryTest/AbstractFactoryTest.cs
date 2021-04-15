using Xunit;
using System;
using AbstractFactory;

namespace AbstractFactoryTest
{
    public class AbstractFactoryTest
    {
        [Theory]
        [ClassData(typeof(AbstractFactoryTestCars))]
        public void Should_crate_a_car_of_the_specified_type(IVehicleFactory vehicleFactory, Type expectedType)
        {
            ICar result = vehicleFactory.CreateCar();
            Assert.IsType(expectedType, result);
        }
        [Theory]
        [ClassData(typeof(AbstractFactoryTestBikes))]
        public void Should_create_a_Bike_of_the_specfied_type(IVehicleFactory vehicleFactory, Type expectedType)
        {
            IBike result = vehicleFactory.CreateBike();
            Assert.IsType(expectedType, result);
        }
    }
    public class AbstractFactoryTestCars : AbstractFactoryBaseTestData
    {
        public AbstractFactoryTestCars()
        {
            AddTestData<LowGradeVehicleFactory, LowGradeCar>();
            AddTestData<HighGradeVehicleFactory, HighGradeCar>();
            AddTestData<MiddleEndVehicleFactory, MiddleEndCar>();
        }
    }
    public class AbstractFactoryTestBikes : AbstractFactoryBaseTestData
    {
        public AbstractFactoryTestBikes()
        {
            AddTestData<LowGradeVehicleFactory, LowGradeBike>();
            AddTestData<HighGradeVehicleFactory, HighGradeBike>();
            AddTestData<MiddleEndVehicleFactory, MiddleEndBike>();
        }
    }
}
