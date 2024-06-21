
namespace AssetLoader.Archive 
{
    /// <summary>
    /// Represents an object that can be read from bytes.
    /// </summary>
    /// <typeparam name="TReadable">The type of object to read.</typeparam>
    public interface IByteReadable<TReadable> where TReadable : IByteReadable<TReadable>
    {
        /// <summary>
        /// Reads an object from bytes
        /// </summary>
        public static TReadable LoadFromBytes(byte[] bytes) 
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Gets the size of the object in bytes. If the object doesn't have a fixed size, it will return cero.
        /// </summary>
        public static int GetSize() 
        {
            throw new System.NotImplementedException();
        }
    }
}
