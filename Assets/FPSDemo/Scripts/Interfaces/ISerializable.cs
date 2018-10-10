namespace FPSDemo
{
    public interface ISerializable
    {
        string SerializedName { get; }
        
        SerializableObject Serialize();
        void Unserialize(SerializableObject serializableObject);
    }
}