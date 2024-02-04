namespace Serialization
{
    public interface ISerializer<T>
    {
        string Serialize(T input);
    }
}
