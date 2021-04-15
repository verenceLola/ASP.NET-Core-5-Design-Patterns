namespace AbstractFactory
{
    public interface IBike { }
    public interface ICar { }
    public interface IVehicleFactory
    {
        ICar CreateCar();
        IBike CreateBike();
    }
}
