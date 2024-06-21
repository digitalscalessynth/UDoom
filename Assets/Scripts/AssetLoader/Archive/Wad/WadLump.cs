using System;
using System.Text;
using AssetLoader.Common;
using AssetLoader.File;

namespace AssetLoader.Archive.Wad 
{
    public class WadLumpException : Exception
    {
        public WadLumpException(string message) : base(message) { }
        public WadLumpException(string message, Exception innerException) : base(message, innerException) { }
    }

    /// <summary>
    /// Represents a lump in a WAD file.
    /// </summary>
    public class WadLump : Entry, IByteReadable<WadLump>
    {
        private const int WadLumpSize = 16;

        public WadLump(string name, uint offset, uint size) : base(name, offset, size) {}

        public void SetName(string name) => Name = name;

        #region Implementation of IByteReadable<T>

        public static WadLump LoadFromBytes(byte[] bytes) 
        {
            // Check if the array has the correct size
            if (bytes.Length < WadLumpSize) 
            {
                throw new WadLumpException("The byte array is too small to be a WAD lump.");
            }

            int offset = BitConverter.ToInt32(bytes, 0);
            int size = BitConverter.ToInt32(bytes, 4);
            string name = Encoding.ASCII.GetString(bytes, 8, 8).TrimEnd('\0');

            return new WadLump(name, (uint)offset, (uint)size);
        }

        public static int GetSize() => WadLumpSize;

        #endregion
    }
}
