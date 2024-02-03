namespace Serialization
{
    public interface ISerializer
    {
        string Serialize(Tracing.TraceResult input);
    }
}
