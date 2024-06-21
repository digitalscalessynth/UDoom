using System;
using System.Text;
using AssetLoader.Common;
using AssetLoader.File;

namespace AssetLoader.Archive.Wad 
{
    public class WadHeaderException : Exception
    {
        public WadHeaderException(string message) : base(message) { }
        public WadHeaderException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class WadHeader : Header, IByteReadable<WadHeader>
    {
        private const int WadHeaderSize = 12;

        public WadHeader(string identification, uint numEntries, uint directoryOffset) : base(identification, numEntries, directoryOffset) {}


        #region Implementation of IByteReadable<T>

        public static WadHeader LoadFromBytes(byte[] bytes) 
        {
            // Check if the array has the correct size
            if (bytes.Length < WadHeaderSize) 
            {
                throw new WadHeaderException("The byte array is too small to be a WAD header.");
            }

            string identification = Encoding.ASCII.GetString(bytes, 0, 4).TrimEnd('\0');
            int numLumps = BitConverter.ToInt32(bytes, 4);
            int dirOffset = BitConverter.ToInt32(bytes, 8);

            return new WadHeader(identification, (uint)numLumps, (uint)dirOffset);
        }

        public static int GetSize() => WadHeaderSize;

        #endregion
    }
}
