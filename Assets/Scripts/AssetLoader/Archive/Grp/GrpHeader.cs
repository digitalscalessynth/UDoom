using System;
using AssetLoader.Common;
using AssetLoader.File;

namespace AssetLoader.Archive.Grp 
{
    public class GrpHeader : Header, IByteReadable<GrpHeader>
    {
        private const int HeaderSize = 16;

        public GrpHeader(string identification, uint numEntries) : base(identification, numEntries, 0) {}

        public void SetDirectoryOffset(uint directoryOffset)
        {
            DirectoryOffset = directoryOffset;
        }

        #region Implementation of IByteReadable<T>

        public static GrpHeader LoadFromBytes(byte[] bytes)
        {
            string signature = System.Text.Encoding.ASCII.GetString(bytes, 0, 12).TrimEnd('\0');
            uint fileCount = BitConverter.ToUInt32(bytes, 12);

            return new GrpHeader(signature, fileCount);
        }

        public static int GetSize() => HeaderSize;

        #endregion
    }
}
