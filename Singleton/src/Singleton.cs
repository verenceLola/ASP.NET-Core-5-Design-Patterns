namespace Singleton
{
    public class MySingleton
    {
        public static MySingleton Instance { get; } = new MySingleton();
        private MySingleton() { }
    }
}
