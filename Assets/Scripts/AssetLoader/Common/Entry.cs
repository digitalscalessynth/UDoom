
namespace AssetLoader.Common 
{
    /// <summary>
    /// Represents a base entry for many archive files.
    /// </summary>
    public abstract class Entry 
    {
        public string Name { get; protected set; }
        public uint Offset { get; protected set; }
        public uint Size { get; protected set; }

        public Entry(string name, uint offset, uint size) 
        {
            Name = name;
            Offset = offset;
            Size = size;
        } 
    } 
}
