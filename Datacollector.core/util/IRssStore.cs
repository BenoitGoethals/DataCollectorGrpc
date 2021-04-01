namespace Datacollector.core.util
{
    public interface IRssStore
    {
        void Save<T>(T myObject);
        T Load<T>();
    }
}