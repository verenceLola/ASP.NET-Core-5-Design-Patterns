namespace AbstractFactory
{
    public class LowGradeVehicleFactory : IVehicleFactory
    {
        public IBike CreateBike() => new LowGradeBike();
        public ICar CreateCar() => new LowGradeCar();
    }
    public class HighGradeVehicleFactory : IVehicleFactory
    {
        public IBike CreateBike() => new HighGradeBike();
        public ICar CreateCar() => new HighGradeCar();
    }
    public class MiddleEndVehicleFactory : IVehicleFactory
    {
        public IBike CreateBike() => new MiddleEndBike();
        public ICar CreateCar() => new MiddleEndCar();
    }
}
